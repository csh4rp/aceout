import { Component, OnInit, ViewEncapsulation, ViewChild, ComponentFactoryResolver } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Router, ActivatedRoute, RouteReuseStrategy } from '@angular/router';
import { FormGroup, FormControl, Validators, FormGroupDirective, NgForm } from '@angular/forms';
import { ErrorStateMatcher, ShowOnDirtyErrorStateMatcher, MatSnackBar } from '@angular/material';
import { SingleAnswerComponent } from '../../templates/single-answer/single-answer.component';
import { MaterialTemplate } from '../../templates/material-template.directive';
import { MaterialType } from 'src/app/model/dashboard/lms/material-type';
import { MaterialControl } from '../../templates/material.control';
import { Observable } from 'rxjs';
import { Store } from '@ngrx/store';
import { AppState } from 'src/app/store/app.state';
import { getCategories } from 'src/app/store/dashboard/lms/material-categories/selectors/material-categories.selectors';
import * as MaterialCategoriesActions from 'src/app/store/dashboard/lms/material-categories/actions';
import * as MaterialActions from 'src/app/store/dashboard/lms/materials/actions';
import { MaterialCategory } from 'src/app/store/dashboard/lms/material-categories/models/material-category.model';
import { MaterialDetails } from 'src/app/store/dashboard/lms/materials/models/material-details.model';
import { getMaterial } from 'src/app/store/dashboard/lms/materials/selectors/materials.selectors';
import { ListItem } from 'src/app/model/list-item';
import { assignFormValues } from 'src/app/helpers/forms';
import { EditorSettings } from 'src/app/model/framework/editor-settings.model';

declare var elFinderBrowser;

@Component({
  templateUrl: './materials.edit.component.html'
})
export class MaterialsEditComponent implements OnInit {

  private id: number;
  private materialControl: MaterialControl;

  categories$: Observable<MaterialCategory[]>;
  material$: Observable<MaterialDetails>;
  errors$: Observable<string[]>;

  materialTypes: ListItem[];
  dataForm: FormGroup;
  submitText: string;
  titleText: string;
  settings: any;


  @ViewChild(MaterialTemplate)
  host: MaterialTemplate;

  get form() {
    return this.dataForm.controls;
  }

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private factory: ComponentFactoryResolver,
    private store: Store<AppState>) {
  }

  ngOnInit() {

    this.dataForm = new FormGroup({
      name: new FormControl('', [Validators.required]),
      content: new FormControl('', []),
      type: new FormControl(0, [Validators.required]),
      categoryId: new FormControl(0, [Validators.required]),
      isActive: new FormControl(false,[])
    });

    this.settings = EditorSettings;

    this.categories$ = this.store.select(getCategories);
    this.store.dispatch(new MaterialCategoriesActions.GetMaterialCategoryList());

    this.route.params.subscribe(p => {

      this.id = parseInt(p['id']);

      if (this.id > 0) {

        this.submitText = 'Save changes';
        this.titleText = 'Edit material';

        this.material$ = this.store.select(getMaterial);
        this.store.dispatch(new MaterialActions.GetMaterial(this.id));

        this.material$.subscribe(material => {
          if(material){
            assignFormValues(material, this.dataForm);
            this.typeChanged(material.type);
            this.materialControl.setModels(material.dataModels, material.answerModels);
          }
        });
      }
      else{
        this.submitText = 'Add';
        this.titleText = 'Add material';
      }
    });

    this.materialTypes = [];
    for (let type in MaterialType) {
      if (isNaN(Number(type))) {
        this.materialTypes.push(new ListItem(MaterialType[type], type));
      }
    }

  }

  typeChanged(type: number) {
    switch (type) {
      case MaterialType.SingleAnswer:
        this.changeComponent(SingleAnswerComponent);
        break;

      default:
        this.host.viewContainerRef.clear();
        break;
    }
  }

  private changeComponent(component: any) {

    const componentFactory = this.factory.resolveComponentFactory(component);
    const viewContainerRef = this.host.viewContainerRef;
    viewContainerRef.clear();

    const componentRef = viewContainerRef.createComponent(componentFactory);
    this.materialControl = <MaterialControl>componentRef.instance;

  }

  private getMaterial(): MaterialDetails {
    const material = <MaterialDetails>this.dataForm.value;
    material.dataModels = this.materialControl.getDataModels();
    material.answerModels = this.materialControl.getAnswerModels();
    material.id = this.id;
    return material;
  }

  saveChanges() {
    const material = this.getMaterial();

    if (this.id === 0) {
      this.store.dispatch(new MaterialActions.AddMaterial(material));
    }
    else {
      this.store.dispatch(new MaterialActions.UpdateMaterial(material));
    }

  }

  back() {
    this.router.navigate(['dashboard', 'lms', 'materials']);
  }

}



