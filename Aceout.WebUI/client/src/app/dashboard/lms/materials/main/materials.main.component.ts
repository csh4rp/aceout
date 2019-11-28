import { Component, OnInit } from '@angular/core';
import { GridControl } from 'src/app/controls/gridControl';
import { TranslateService } from '@ngx-translate/core';
import { MatDialog, MatSnackBar } from '@angular/material';
import { Router } from '@angular/router';
import { ButtonRowRenderer } from 'src/app/shared-dashboard/grid/buttons/button-row-renderer.component';
import { DialogComponent } from 'src/app/shared-dashboard/dialog/dialog.component';
import { MaterialService } from 'src/app/services/dashboard/lms/material.service';
import { GridDataSource } from 'src/app/model/gridDataSource';
import { SortModel } from 'src/app/model/sortModel';
import * as MaterialActions from 'src/app/store/dashboard/lms/materials/actions';
import { Store } from '@ngrx/store';
import { AppState } from 'src/app/store/app.state';
import { Observable } from 'rxjs';
import { getMaterialsLoaded } from 'src/app/store/dashboard/lms/materials/selectors/materials.selectors';


@Component({
  templateUrl: './materials.main.component.html'
})
export class MaterialsMainComponent extends GridControl implements OnInit {

  frameworkComponents: any;
  buttons: any[];
  columns: any[];
  isLoaded$: Observable<boolean>;
  gridApi: any;

  constructor(translate: TranslateService,
    private materialService: MaterialService,
    private router: Router,
    private dialog: MatDialog,
    private store: Store<AppState>) {
    super(translate);

  }

  ngOnInit() {
    this.buttons = [
      {
        name: 'edit',
        action: (p) => this.router.navigate(['/dashboard/lms/materials/edit/' + p]),
        icon: 'edit',
        data: 'id'
      },
      {
        name: 'delete',
        action: (p) => {
          const dialogRef = this.dialog.open(DialogComponent, {
            width: '400px', data:
              { title: 'Delete material', content: 'Do you really want to delete selected material?' }
          });
          dialogRef.afterClosed().subscribe(result => {
            if (result) {
              this.deleteMaterial(p);
            }
          })
        },
        icon: 'trash',
        data: 'id'
      }
    ];

    this.columns = [
      { colId: 'Name', headerName: 'Name', field: 'name' },
      {
        colId: 'button', cellRenderer: 'buttonRowRenderer', headerName: '', field: 'id',
        width: 90, suppressSizeToFit: true,
        cellRendererParams: { buttons: this.buttons }
      }
    ];

    this.frameworkComponents = {
      buttonRowRenderer: ButtonRowRenderer
    };

    this.isLoaded$ = this.store.select(getMaterialsLoaded);
    this.isLoaded$.subscribe(isLoaded => {
      if (isLoaded && this.gridApi) {
        this.changeDs();
      }
    });
  }

  private redirect(param: any) {
    return '/dashboard/lms/materials/edit/' + param;
  }


  public gridReady(params) {
    this.gridApi = params.api;
    super.onGridReady(params, this.columns);
    let ds = new GridDataSource(this.materialService, new SortModel("id", "asc"));

    this.gridApi.setDatasource(ds);
    this.gridApi.sizeColumnsToFit();
  }

  public changeDs() {
    let ds = new GridDataSource(this.materialService, new SortModel("id", "asc"));
    this.gridApi.setDatasource(ds);
  }

  public addMaterial() {
    this.router.navigate([this.redirect(0)]);
  }

  private deleteMaterial(id: number) {
    this.store.dispatch(new MaterialActions.DeleteMaterial(id));
  }
}

