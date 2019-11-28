import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { HttpClient } from '@angular/common/http';
import { UrlHelper } from 'src/app/app.urls';
import { ButtonRowRenderer } from 'src/app/shared-dashboard/grid/buttons/button-row-renderer.component';
import { Router } from '@angular/router';
import { GridDataSource } from 'src/app/model/gridDataSource';
import { GridControl } from 'src/app/controls/gridControl';
import { SortModel } from 'src/app/model/sortModel';
import { MatDialog, MatSnackBar } from '@angular/material';
import { DialogComponent } from 'src/app/shared-dashboard/dialog/dialog.component';
import { CoursePathService } from 'src/app/services/dashboard/lms/course-path.service';
import { CourseService } from 'src/app/services/dashboard/lms/course.service';
import { Observable } from 'rxjs';
import { Store } from '@ngrx/store';
import { AppState } from 'src/app/store/app.state';
import { getCourse, getCoursesLoaded } from 'src/app/store/dashboard/lms/courses/selectors/courses.selectors';
import * as CourseActions from 'src/app/store/dashboard/lms/courses/actions';

@Component({
  templateUrl: './courses.main.component.html'
})
export class CoursesMainComponent extends GridControl implements OnInit {

  frameworkComponents: any;
  columns: any[];
  buttons: any[];
  isLoaded$: Observable<boolean>;
  gridApi: any;

  constructor(translate: TranslateService,
    private courseService: CourseService,
    private router: Router,
    private dialog: MatDialog,
    private store: Store<AppState>) {
    super(translate);

  }

  ngOnInit() {
    this.buttons = [
      {
        name: 'edit',
        action: (p) => this.router.navigate(['/dashboard/lms/courses/edit/' + p]),
        icon: 'edit',
        data: 'id'
      },
      {
        name: 'delete',
        action: (p) => {
          const dialogRef = this.dialog.open(DialogComponent,
            { width: '400px', data: { title: 'Delete course', content: 'Do you really want to delete selected course?' } });
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

    this.isLoaded$ = this.store.select(getCoursesLoaded);
    this.isLoaded$.subscribe(isLoaded => {
      if (isLoaded && this.gridApi) {
        this.changeDs();
      }
    })
  }

  redirect(param: any) {
    return '/dashboard/lms/courses/edit/' + param;
  }

  onGridReady(params) {
    this.gridApi = params.api;
    super.onGridReady(params, this.columns);
    let ds = new GridDataSource(this.courseService, new SortModel("id", "asc"));
    this.gridApi.setDatasource(ds);
    this.gridApi.sizeColumnsToFit();
  }

  changeDs() {
    let ds = new GridDataSource(this.courseService, new SortModel("id", "asc"));
    this.gridApi.setDatasource(ds);
  }

  add() {
    this.router.navigate([this.redirect(0)]);
  }

  deleteCategory(id: number) {
    this.store.dispatch(new CourseActions.DeleteCourse(id));
  }
}

