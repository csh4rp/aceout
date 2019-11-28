import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { HttpClient } from '@angular/common/http';
import { UrlHelper } from 'src/app/app.urls';
import { ButtonRowRenderer } from 'src/app/shared-dashboard/grid/buttons/button-row-renderer.component';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/dashboard/administration/user.service';
import { GridDataSource } from 'src/app/model/gridDataSource';
import { GridControl } from 'src/app/controls/gridControl';
import { SortModel } from 'src/app/model/sortModel';
import { MatDialog, MatSnackBar } from '@angular/material';
import { DialogComponent } from 'src/app/shared-dashboard/dialog/dialog.component';
import { Store } from '@ngrx/store';
import { AppState } from 'src/app/store/app.state';
import * as CategoryActions from 'src/app/store/dashboard/lms/material-categories/actions';
import { Observable } from 'rxjs';
import { getCategoryLoaded } from 'src/app/store/dashboard/lms/material-categories/selectors/material-categories.selectors';
import { MaterialCategoriesService } from 'src/app/store/dashboard/lms/material-categories/services/material-categories.service';

@Component({
  templateUrl: './categories.main.component.html'
})
export class CategoriesMainComponent extends GridControl implements OnInit {

  frameworkComponents: any;
  buttons: any[];
  colums: any[];
  isLoaded$: Observable<boolean>;
  columns: any[];
  gridApi: any;

  constructor(translate: TranslateService,
    private categoryService: MaterialCategoriesService,
    private router: Router,
    private dialog: MatDialog,
    private store: Store<AppState>) {
    super(translate);

  }

  ngOnInit() {
    this.buttons = [
      {
        name: 'edit',
        action: (p) => this.router.navigate(['/dashboard/lms/categories/edit/' + p]),
        icon: 'edit',
        data: 'id'
      },
      {
        name: 'delete',
        action: (p) => {
          const dialogRef = this.dialog.open(DialogComponent,
            { width: '400px', data: { title: 'Delete category', content: 'Dou you really want to delete selected category?' } });
          dialogRef.afterClosed().subscribe(result => {
            if (result) {
              this.deleteCategory(p);
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

    this.isLoaded$ = this.store.select(getCategoryLoaded);

    this.isLoaded$.subscribe(isLoaded => {
      if (isLoaded && this.gridApi) {
        this.changeDs();
      }
    })
  }

  redirect(param: any) {
    return '/dashboard/lms/categories/edit/' + param;
  }

  onGridReady(params) {
    this.gridApi = params.api;
    super.onGridReady(params, this.columns);
    let ds = new GridDataSource(this.categoryService, new SortModel("id", "asc"));
    this.gridApi.setDatasource(ds);
    this.gridApi.sizeColumnsToFit();
  }

  changeDs() {
    const ds = new GridDataSource(this.categoryService, new SortModel("id", "asc"));
    this.gridApi.setDatasource(ds);
  }

  addCategory() {
    this.router.navigate([this.redirect(0)]);
  }

  deleteCategory(id: number) {
    this.store.dispatch(new CategoryActions.DeleteMaterialCategory(id));
  }
}

