import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { ButtonRowRenderer } from 'src/app/shared-dashboard/grid/buttons/button-row-renderer.component';
import { Router } from '@angular/router';
import { GridDataSource } from 'src/app/model/gridDataSource';
import { GridControl } from 'src/app/controls/gridControl';
import { SortModel } from 'src/app/model/sortModel';
import { MatSnackBar, MatDialog } from '@angular/material';
import { DialogComponent } from 'src/app/shared-dashboard/dialog/dialog.component';
import { GroupService } from 'src/app/services/dashboard/organization/group.service';
import { Observable } from 'rxjs';
import { Store } from '@ngrx/store';
import { AppState } from 'src/app/store/app.state';
import * as  GroupActions from 'src/app/store/dashboard/organization/groups/actions';
import { getGroupsLoaded } from 'src/app/store/dashboard/organization/groups/selectors/groups.selectos';

@Component({
  templateUrl: './groups.main.component.html'
})
export class GroupsMainComponent extends GridControl implements OnInit {

  frameworkComponents: any;
  buttons: any[];
  isLoaded$: Observable<boolean>;
  columns: any[];
  gridApi: any;

  constructor(translate: TranslateService,
    private groupService: GroupService,
    private router: Router,
    private dialog: MatDialog,
    private store: Store<AppState>) {
    super(translate);
  }

  ngOnInit() {
    this.buttons = [
      {
        name: 'edit',
        action: (p) => this.router.navigate(['/dashboard/organization/groups/edit/' + p]),
        icon: 'edit',
        data: 'id'
      },
      {
        name: 'delete',
        action: (p) => {
          const dialogRef = this.dialog.open(DialogComponent,
            { width: '400px', data: { title: 'Delete group', content: 'Dou you really want to delete selected group?' } });
          dialogRef.afterClosed().subscribe(result => {
            if (result) {
              this.deleteUser(p);
            }
          })
        },
        icon: 'trash',
        data: 'id'
      }];

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

    this.isLoaded$ = this.store.select(getGroupsLoaded);

    this.isLoaded$.subscribe(isLoaded => {
      if (isLoaded && this.gridApi) {
        this.changeDs();
      }
    })
  }

  redirect(param: any) {
    return '/dashboard/organization/groups/edit/' + param;
  }

  onGridReady(params) {
    this.gridApi = params.api;
    super.onGridReady(params, this.columns);
    let ds = new GridDataSource(this.groupService, new SortModel("id", "asc"));

    this.gridApi.setDatasource(ds);
    this.gridApi.sizeColumnsToFit();
  }

  changeDs() {
    let ds = new GridDataSource(this.groupService, new SortModel("id", "asc"));
    this.gridApi.setDatasource(ds);
  }

  add() {
    this.router.navigate([this.redirect(0)]);
  }

  deleteUser(id: number) {
    this.store.dispatch(new GroupActions.DeleteGroup(id));
  }
}

