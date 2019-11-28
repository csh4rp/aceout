import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormControl, Validators, FormGroupDirective, NgForm } from '@angular/forms';
import { ErrorStateMatcher, ShowOnDirtyErrorStateMatcher, MatSnackBar } from '@angular/material';
import { FormAssigner } from 'src/app/helpers/formAssigner';
import { SnackBarComponent } from 'src/app/shared-dashboard/snack-bar/snack-bar.component';
import { Location } from '@angular/common';
import { Store } from '@ngrx/store';
import { AppState } from 'src/app/store/app.state';
import { MaterialCategory } from 'src/app/store/dashboard/lms/material-categories/models/material-category.model';
import { Observable } from 'rxjs';
import { getCategory } from 'src/app/store/dashboard/lms/material-categories/selectors/material-categories.selectors';
import * as CategoryActions from 'src/app/store/dashboard/lms/material-categories/actions';
import { assignFormValues } from 'src/app/helpers/forms';

@Component({
  templateUrl: './categories.edit.component.html'
})
export class CategoriesEditComponent implements OnInit {

  dataForm: FormGroup;
  category$: Observable<MaterialCategory>;
  submitText: string;
  titleText: string;

  private id: number;

  constructor(private route: ActivatedRoute,
    private router: Router,
    private store: Store<AppState>) {

  }

  ngOnInit() {
    this.dataForm = new FormGroup({
      name: new FormControl('', [Validators.required])
    });

    this.route.params.subscribe(p => {
      this.id = parseInt(p['id']);

      if (this.id > 0) {
        this.submitText = 'Save changes';
        this.titleText = 'Edit category';

        this.category$ = this.store.select(getCategory);

        this.category$.subscribe(category => {
          if (category) {
            assignFormValues(category, this.dataForm);
          }
        });

        this.store.dispatch(new CategoryActions.GetMaterialCategory(this.id));
      }
      else {
        this.submitText = 'Add';
        this.titleText = 'Add category';
      }
    });
  }

  get form() { return this.dataForm.controls; }

  getCategory(): MaterialCategory {
    const category = this.dataForm.value as MaterialCategory;
    category.id = this.id;

    return category;
  }

  onSubmit() {
    const category = this.getCategory();

    if (this.id === 0) {
      this.store.dispatch(new CategoryActions.AddMaterialCategory(category));
    }
    else {
      this.store.dispatch(new CategoryActions.UpdateMaterialCategory(category));
    }
  }
  back() {
    this.router.navigate(['dashboard', 'lms', 'categories']);
  }

}

