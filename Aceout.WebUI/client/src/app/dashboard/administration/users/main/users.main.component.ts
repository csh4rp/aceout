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
import { ModalMessageStore, ModalMessage } from 'src/app/model/modalMessageStore';
import { MatSnackBar, MatDialog } from '@angular/material';
import { DialogComponent } from 'src/app/shared-dashboard/dialog/dialog.component';
import { Store } from '@ngrx/store';
import { AppState } from 'src/app/store/app.state';
import * as UserActions from 'src/app/store/dashboard/administration/users/actions';
import { Observable } from 'rxjs';
import { getUserLoaded } from 'src/app/store/dashboard/administration/users/selectors/users.selectors';

@Component({
  templateUrl: './users.main.component.html'
})
export class UsersMainComponent extends GridControl implements OnInit {

  frameworkComponents;
  isLoaded$: Observable<boolean>;
  buttons: any[];
  columns: any[];
  gridApi: any;


  constructor(translate: TranslateService,
    private userService: UserService,
    private router: Router,
    private dialog: MatDialog,
    private store: Store<AppState>) {
    super(translate);

  }

  ngOnInit() {
    this.isLoaded$ = this.store.select(getUserLoaded);
    this.isLoaded$.subscribe(isLoaded => {
      if(isLoaded && this.gridApi){
        this.changeDs();
      }
    });

    this.buttons = [
      {
        name: 'edit',
        action: (p) => this.router.navigate(['/dashboard/administration/users/edit/' + p]),
        icon: 'edit',
        data: 'id'
      },
      {
        name: 'delete',
        action: (p) => {
          const dialogRef = this.dialog.open(DialogComponent,
            { width: '400px', data: { title: 'Delete user', content: 'Are you sure you want to delete selected user?' } });
          dialogRef.afterClosed().subscribe(result => {
            if (result) {
              this.deleteUser(p);
            }
          })
        },
        icon: 'trash',
        data: 'id'
      }
    ];

    this.frameworkComponents = {
      buttonRowRenderer: ButtonRowRenderer
    };

    this.columns = [
      { colId: 'Username', headerName: 'Username', field: 'userName' },
      { colId: 'First Name', headerName: 'First Name', field: 'firstName' },
      { colId: 'Last Name', headerName: 'Last Name', field: 'lastName' },
      {
        colId: 'button', cellRenderer: 'buttonRowRenderer', headerName: '', field: 'id',
        width: 90, suppressSizeToFit: true,
        cellRendererParams: { buttons: this.buttons }
      }
    ];
  }

  redirect(param: any) {
    return '/dashboard/administration/users/edit/' + param;
  }

  gridReady(params) {
    this.gridApi = params.api;
    super.onGridReady(params, this.columns);
    let ds = new GridDataSource(this.userService, new SortModel("id", "asc"));

    this.gridApi.setDatasource(ds);
    this.gridApi.sizeColumnsToFit();
  }

  changeDs() {
    if (this.gridApi) {
      const ds = new GridDataSource(this.userService, new SortModel("id", "asc"));
      this.gridApi.setDatasource(ds);
    }
  }

  addUser() {
    this.router.navigate([this.redirect(0)]);
  }

  deleteUser(id: number) {
    this.store.dispatch(new UserActions.DeleteUser(id));
  }
}

