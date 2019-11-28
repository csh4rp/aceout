import { Component, OnInit } from "@angular/core";
import { GridControl } from "src/app/controls/gridControl";
import { TranslateService } from "@ngx-translate/core";
import { CourseReportsService } from "src/app/store/dashboard/lms/reports/services/course-reports.service";
import { GridDataSource } from "src/app/model/gridDataSource";
import { SortModel } from "src/app/model/sortModel";
import { Router, ActivatedRoute } from "@angular/router";
import { ButtonRowRenderer } from "src/app/shared-dashboard/grid/buttons/button-row-renderer.component";
import { LessonsService } from "src/app/store/dashboard/lms/lessons/services/lessons.service";
import { formatHelper } from "src/app/helpers/formatHelper";

@Component({
    templateUrl: './course-reports.component.html'
})
export class CourseReportsComponent extends GridControl implements OnInit {

    private id: number;
    private coursesGridApi: any;
    private lessonsGridApi: any;
    private coursesColumnGridApi: any;
    private lessonsColumnGridApi: any;

    courseColumns: any[] = [
        { colId: 'firstName', headerName: 'First name', field: 'firstName' }
    ];
    lessonColumns: any[];
    lessonButtons: any[];
    frameworkComponents: any;

    constructor(private translateService: TranslateService,
        private service: CourseReportsService,
        private lessonsService: LessonsService,
        private route: ActivatedRoute,
        private router: Router) {
        super(translateService);
    }

    public ngOnInit() {

        this.route.params.subscribe(p => {
            this.id = parseInt(p['courseId']);
        });

        this.lessonButtons = [
            {
                name: 'edit',
                action: p => this.router.navigate(['/dashboard/lms/reports/', this.id, p]),
                icon: 'file-alt',
                data: 'id'
            }
        ];

        this.lessonColumns = [
            { colId: 'Name', headerName: 'Name', field: 'name' },
            {
                colId: 'button', cellRenderer: 'buttonRowRenderer', headerName: '', field: 'id',
                width: 90, suppressSizeToFit: true,
                cellRendererParams: { buttons: this.lessonButtons }
            }
        ];

        this.courseColumns = [
            { colId: 'FirstName', headerName: 'First Name', field: 'firstName' },
            { colId: 'LastName', headerName: 'Last Name', field: 'lastName' },
            { colId: 'Result', headerName: 'Result', field: 'result', valueFormatter: resultFormatter},
            { colId: 'StartedDate', headerName: 'Started date', field: 'startedDate', valueFormatter: dateFormatter},
            { colId: 'CompletedDate', headerName: 'Completed date', field: 'completedDate', valueFormatter: dateFormatter},
            { colId: 'Attempt', headerName: 'Attempt', field: 'attempt'},
        ];

        this.frameworkComponents = {
            buttonRowRenderer: ButtonRowRenderer
        };
    }

    onCoursesGridReady(params) {
        super.onGridReady(params, this.courseColumns);
        this.coursesGridApi = params.api;
        this.coursesColumnGridApi = params.columnApi;

        const ds = new GridDataSource(this.service, new SortModel("userName", "asc"), { courseId: this.id });
        this.coursesGridApi.setDatasource(ds);
        this.coursesGridApi.sizeColumnsToFit();
    }

    onLessonsGridReady(params) {
        super.onGridReady(params, this.lessonColumns);
        this.lessonsGridApi = params.api;
        this.lessonsColumnGridApi = params.columnApi;

        const ds = new GridDataSource(this.lessonsService, new SortModel("id", "asc"), { courseId: this.id });
        this.lessonsGridApi.setDatasource(ds);
        params.api.sizeColumnsToFit();
    }

    back() {
        this.router.navigate(['dashboard', 'lms', 'courses', 'edit', this.id]);
    }


}

function resultFormatter(param) {
    return formatHelper.toPercentage(param.value);
}

function dateFormatter(param){
    if(!param.value) return '-';
    return formatHelper.formatDateString(param.value);
}
