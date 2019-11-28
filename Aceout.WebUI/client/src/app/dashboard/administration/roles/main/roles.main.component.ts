import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { HttpClient } from '@angular/common/http';
import { UrlHelper } from 'src/app/app.urls';
import { Router } from '@angular/router';
import { GridDataSource } from 'src/app/model/gridDataSource';
import { GridControl } from 'src/app/controls/gridControl';
import { SortModel } from 'src/app/model/sortModel';
import { RoleService } from 'src/app/services/dashboard/administration/role.service';
import { ButtonRowRenderer } from 'src/app/shared-dashboard/grid/buttons/button-row-renderer.component';
import { ModalMessageStore, ModalMessage } from 'src/app/model/modalMessageStore';
import { MatDialogRef, MatDialog, MatSnackBar } from '@angular/material';
import { DialogComponent } from 'src/app/shared-dashboard/dialog/dialog.component';
import { Store } from '@ngrx/store';
import { AppState } from 'src/app/store/app.state';
import * as RoleActions from 'src/app/store/dashboard/administration/roles/actions';
import { Observable } from 'rxjs';
import { getRolesLoaded } from 'src/app/store/dashboard/administration/roles/selectors/roles.selectors';

@Component({
  templateUrl: './roles.main.component.html'
})
export class RolesMainComponent extends GridControl implements OnInit {


  frameworkComponents;
  buttons: any[];
  isLoaded$: Observable<boolean>;
  columns: any[];
  gridApi: any;


  constructor(translate: TranslateService,
    private roleService: RoleService,
    private router: Router,
    private dialog: MatDialog,
    private store: Store<AppState>) {
    super(translate);
  }

  ngOnInit() {

    this.isLoaded$ = this.store.select(getRolesLoaded);
    this.isLoaded$.subscribe(isLoaded => {
      if(isLoaded && this.gridApi){
        this.changeDs();
      }
    });


    this.buttons = [
      {
        name: 'name',
        action: (p) => this.router.navigate(['/dashboard/administration/roles/edit/' + p]),
        icon: 'edit',
        data: 'id'
      },
      {
        name: 'delete',
        action: (p) => {
          const dialogRef = this.dialog.open(DialogComponent,
            { width: '400px', data: { title: 'Delete role', content: 'Do your really want to delete selected role?' } });
          dialogRef.afterClosed().subscribe(result => {
            if (result) {
              this.deleteRole(p);
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
        colId: 'button', cellRenderer: 'editRowRenderer', headerName: '', field: 'id',
        width: 70, suppressSizeToFit: true,
        cellRendererParams: { buttons: this.buttons }
      }
    ];

    this.frameworkComponents = {
      editRowRenderer: ButtonRowRenderer
    };
  }

  redirect(param: any) {
    return '/dashboard/administration/roles/edit/' + param;
  }

  onGridReady(params) {
    this.gridApi = params.api;
    super.onGridReady(params, this.columns);
    let ds = new GridDataSource(this.roleService, new SortModel("id", "asc"));

    this.gridApi.setDatasource(ds);
    this.gridApi.sizeColumnsToFit();
  }

  changeDs() {
    let ds = new GridDataSource(this.roleService, new SortModel("id", "asc"));
    this.gridApi.setDatasource(ds);
  }

  addRole() {
    this.router.navigate([this.redirect(0)]);
  }

  deleteRole(id: number) {
    this.store.dispatch(new RoleActions.DeleteRole(id));
  }
}

