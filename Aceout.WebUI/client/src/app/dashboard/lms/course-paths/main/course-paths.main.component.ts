import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { ButtonRowRenderer } from 'src/app/shared-dashboard/grid/buttons/button-row-renderer.component';
import { Router } from '@angular/router';
import { GridDataSource } from 'src/app/model/gridDataSource';
import { GridControl } from 'src/app/controls/gridControl';
import { SortModel } from 'src/app/model/sortModel';
import { MatDialog, MatSnackBar } from '@angular/material';
import { DialogComponent } from 'src/app/shared-dashboard/dialog/dialog.component';
import { Observable } from 'rxjs';
import { Store } from '@ngrx/store';
import { AppState } from 'src/app/store/app.state';
import { getCoursePathsLoaded } from 'src/app/store/dashboard/lms/course-paths/selectors/course-paths.selectors';
import * as CoursePathActions from 'src/app/store/dashboard/lms/course-paths/actions';
import { CoursePathsService } from 'src/app/store/dashboard/lms/course-paths/services/course-paths.service';

@Component({
  templateUrl: './course-paths.main.component.html'
})
export class CoursePathsMainComponent extends GridControl implements OnInit {


  frameworkComponents: any;
  columns: any[];
  buttons: any[];
  isLoaded$: Observable<boolean>;
  gridApi: any;

  constructor(translate: TranslateService,
    private coursePathService: CoursePathsService,
    private router: Router,
    private dialog: MatDialog,
    private store: Store<AppState>) {
    super(translate);

  }

  redirect(param: any) {
    return '/dashboard/lms/course-paths/edit/' + param;
  }

  onGridReady(params) {
    this.gridApi = params.api;
    super.onGridReady(params, this.columns);
    const ds = new GridDataSource(this.coursePathService, new SortModel("id", "asc"));
    this.gridApi.setDatasource(ds);
    this.gridApi.sizeColumnsToFit();
  }

  changeDs() {
    const ds = new GridDataSource(this.coursePathService, new SortModel("id", "asc"));
    this.gridApi.setDatasource(ds);
  }

  add() {
    this.router.navigate([this.redirect(0)]);
  }

  deleteCategory(id: number) {
    this.store.dispatch(new CoursePathActions.DeleteCoursePath(id));
  }

  ngOnInit() {
    this.buttons = [
      {
        name: 'edit',
        action: (p) => this.router.navigate(['/dashboard/lms/course-paths/edit/' + p]),
        icon: 'edit',
        data: 'id'
      },
      {
        name: 'delete',
        action: (p) => {
          const dialogRef = this.dialog.open(DialogComponent,
            { width: '400px', data: { title: 'Delete course path', content: 'Do your really want to delete selected course path?' } });
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

    this.isLoaded$ = this.store.select(getCoursePathsLoaded);
    this.isLoaded$.subscribe(isLoaded => {
      if (isLoaded && this.gridApi) {
        this.changeDs();
      }
    })
  }
}

