import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { ButtonRowRenderer } from 'src/app/shared-dashboard/grid/buttons/button-row-renderer.component';
import { Router, ActivatedRoute } from '@angular/router';
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
import { LessonReportsService } from 'src/app/store/dashboard/lms/reports/services/lesson-reports.service';
import { formatHelper } from 'src/app/helpers/formatHelper';

@Component({
    templateUrl: './lesson-reports.component.html'
})
export class LessonReportsComponent extends GridControl implements OnInit {

    columns: any[];
    gridApi: any;
    private lessonId: number;
    private courseId: number;

    constructor(translate: TranslateService,
        private service: LessonReportsService,
        private route: ActivatedRoute,
        private router: Router) {
        super(translate);

    }

    ngOnInit() {
        this.route.params.subscribe(p => {
            this.lessonId = parseInt(p['lessonId']);
            this.courseId = parseInt(p['courseId']);
        });

        this.columns = [
            { colId: 'FirstName', headerName: 'First Name', field: 'firstName' },
            { colId: 'LastName', headerName: 'Last Name', field: 'lastName' },
            { colId: 'Result', headerName: 'Result', field: 'result', valueFormatter: resultFormatter},
            { colId: 'StartedDate', headerName: 'Started date', field: 'startedDate', valueFormatter: dateFormatter},
            { colId: 'CompletedDate', headerName: 'Completed date', field: 'completedDate', valueFormatter: dateFormatter},
            { colId: 'Attempt', headerName: 'Attempt', field: 'attempt'},
        ];
    }

    redirect(param: any) {
        return '/dashboard/lms/courses/edit/' + param;
    }

    gridReady(params) {
        this.gridApi = params.api;
        super.onGridReady(params, this.columns);
        let ds = new GridDataSource(this.service, new SortModel("userName", "asc"), { lessonId: this.lessonId });
        this.gridApi.setDatasource(ds);
        this.gridApi.sizeColumnsToFit();
    }

    back() {
        this.router.navigate(['dashboard', 'lms', 'reports', this.courseId]);
    }
}

function resultFormatter(param) {
    return formatHelper.toPercentage(param.value);
}

function dateFormatter(param){
    if(!param.value) return '-';
    return formatHelper.formatDateString(param.value);
}
