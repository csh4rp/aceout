import { Component, OnInit, ViewEncapsulation, NgZone } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { HttpClient } from '@angular/common/http';
import { UrlHelper } from 'src/app/app.urls';
import { ButtonRowRenderer } from 'src/app/shared-dashboard/grid/buttons/button-row-renderer.component';
import { Router, ActivatedRoute } from '@angular/router';
import { GridControl } from 'src/app/controls/gridControl';
import { SortModel } from 'src/app/model/sortModel';
import { FormGroup, FormControl, Validators, FormGroupDirective, NgForm } from '@angular/forms';
import { ErrorStateMatcher, ShowOnDirtyErrorStateMatcher, MatSnackBar } from '@angular/material';
import { CoursePathService } from 'src/app/services/dashboard/lms/course-path.service';
import { CourseService } from 'src/app/services/dashboard/lms/course.service';
import { GroupService } from 'src/app/services/dashboard/organization/group.service';
import { Lesson } from 'src/app/model/dashboard/lms/lessons/lesson';
import { CourseDetails } from 'src/app/store/dashboard/lms/courses/models/course-details.model';
import { formArrayNameProvider } from '@angular/forms/src/directives/reactive_directives/form_group_name';
import { Store } from '@ngrx/store';
import { AppState } from 'src/app/store/app.state';
import * as  CourseActions from 'src/app/store/dashboard/lms/courses/actions';
import * as  CoursePathActions from 'src/app/store/dashboard/lms/course-paths/actions';
import { Observable } from 'rxjs';
import { CoursePath } from 'src/app/store/dashboard/lms/course-paths/models/course-path.model';
import { getCoursePaths } from 'src/app/store/dashboard/lms/course-paths/selectors/course-paths.selectors';
import { getGroups } from 'src/app/store/dashboard/organization/groups/selectors/groups.selectos';
import { Group } from 'src/app/store/dashboard/organization/groups/models/group.model';
import * as GroupActions from 'src/app/store/dashboard/organization/groups/actions';
import { getCourse } from 'src/app/store/dashboard/lms/courses/selectors/courses.selectors';
import { CourseViewModel } from 'src/app/store/dashboard/lms/courses/models/course.view-model';
import { CourseLesson } from 'src/app/store/dashboard/lms/courses/models/course-lesson.model';
import { CourseGroup } from 'src/app/store/dashboard/lms/courses/models/course-group.model';
import { formatHelper } from 'src/app/helpers/formatHelper';
import { NumericEditor } from 'src/app/shared-dashboard/grid/numeric-editor/numeric-editor.component';
import * as moment from 'moment';
import { toLocalDateString, toISODateString } from 'src/app/helpers/dates';
import { getDatePicker } from 'src/app/helpers/native-controls';
import { assignFormValues } from 'src/app/helpers/forms';
import { EditorSettings } from 'src/app/model/framework/editor-settings.model';
declare var $
declare var elFinderBrowser;

@Component({
    templateUrl: './courses.edit.component.html'
})
export class CoursesEditComponent extends GridControl implements OnInit {


    dataForm: FormGroup;
    titleText: string;
    submitText: string;
    buttons: any[];
    groupButtons: any[];
    groupColumns: any[];
    lessonColumns: any[];
    lessonButtons: any[];
    frameworkComponents: any;
    components: any;
    course$: Observable<CourseViewModel>;
    coursePaths$: Observable<CoursePath[]>;
    groups$: Observable<CourseGroup[]>;

    settings = EditorSettings;

    private id: number;
    private groupsGridApi;
    private lessonsGridApi;
    private groupsGridColumnApi;
    private lessonsGridColumnApi;
    lessons: CourseLesson[];
    groups: CourseGroup[];
    filteredGroups: Group[];


    constructor(translate: TranslateService,
        private courseService: CourseService,
        private coursePathService: CoursePathService,
        private groupService: GroupService,
        private route: ActivatedRoute,
        private router: Router,
        private zone: NgZone,
        private store: Store<AppState>) {
        super(translate);
    }

    navigate(param: any) {
        this.zone.run(() => {
            this.router.navigate(['/dashboard/lms/courses/lesson/', this.id, param]);
        });
    }

    onGroupsGridReady(params) {
        super.onGridReady(params, this.groupColumns);
        this.groupsGridApi = params.api;
        this.groupsGridColumnApi = params.columnApi;

        this.groupsGridApi.setRowData(this.groups);
        params.api.sizeColumnsToFit();
    }

    onLessonsGridReady(params) {
        super.onGridReady(params, this.lessonColumns);
        this.lessonsGridApi = params.api;
        this.lessonsGridColumnApi = params.columnApi;

        this.groupsGridApi.setRowData(this.lessons);
        params.api.sizeColumnsToFit();
    }

    ngOnInit() {

        this.dataForm = new FormGroup({
            name: new FormControl('', Validators.required),
            pictureUrl: new FormControl('', []),
            description: new FormControl('', []),
            coursePathId: new FormControl(0, []),
            fromDate: new FormControl('', []),
            toDate: new FormControl('', []),
            group: new FormControl('', []),
            isActive: new FormControl(false, []),
            requireLessonOrder: new FormControl(false, []),
            passThreshold: new FormControl(null, [])
        });

        this.groupButtons = [
            {
                name: 'delete',
                action: (p) => {
                    this.groupsGridApi.forEachNode((node, index) => {
                        if (node.data.id == p) {
                            const item = node.data as Group;
                            this.groupsGridApi.updateRowData({ remove: [item] });
                        }
                    })
                },
                icon: 'trash',
                data: 'id'
            }
        ];

        this.groupColumns = [
            { colId: 'Name', headerName: 'Name', field: 'name' },
            { colId: 'From date', headerName: 'From date', field: 'fromDate', editable: true, cellEditor: 'datePicker', valueGetter: formatFromDate },
            { colId: 'To date', headerName: 'To date', field: 'toDate', editable: true, cellEditor: 'datePicker', valueGetter: formatToDate },
            { colId: 'Attempts', headerName: 'Attempts', field: 'attemptCount', editable: true, cellEditor: 'numericEditor' },
            {
                colId: 'button', cellRenderer: 'buttonRowRenderer', headerName: '', field: 'id',
                width: 90, suppressSizeToFit: true,
                cellRendererParams: { buttons: this.groupButtons }
            }
        ];

        this.lessonButtons = [
            {
                name: 'edit',
                action: this.navigate.bind(this),
                icon: 'edit',
                data: 'id'
            },
            {
                name: 'delete',
                action: (p) => {
                    this.lessonsGridApi.forEachNode((node, index) => {
                        if (node.data.id === p) {
                            const item = node.data as Lesson;
                            this.lessonsGridApi.updateRowData({ remove: [item] });
                        }
                    })
                },
                icon: 'trash',
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

        this.frameworkComponents = {
            buttonRowRenderer: ButtonRowRenderer,
            numericEditor: NumericEditor
        };

        this.components = {
            datePicker: getDatePicker()
        };


        this.coursePaths$ = this.store.select(getCoursePaths);
        this.store.dispatch(new CoursePathActions.GetCoursePathList());

        this.route.params.subscribe(p => {
            this.id = parseInt(p['id']);

            if (this.id > 0) {
                this.titleText = 'Edit course';
                this.submitText = 'Save changes';

                this.course$ = this.store.select(getCourse);
                this.course$.subscribe(course => {
                    if (course) {
                        assignFormValues(course, this.dataForm);

                        if (course.passThreshold !== null) {
                            this.dataForm.controls['passThreshold'].setValue(course.passThreshold * 100.0);
                        }

                        this.groups = course.groups;
                        this.lessons = course.lessons;

                        if (this.groupsGridApi) {
                            this.groupsGridApi.setRowData(this.groups);
                        }
                        if (this.lessonsGridApi) {
                            this.lessonsGridApi.setRowData(this.lessons);
                        }
                    }
                });

                this.store.dispatch(new CourseActions.GetCourse(this.id));
            }
            else {
                this.titleText = 'Add course';
                this.submitText = 'Add';
            }
        });

        this.dataForm.controls['group'].valueChanges.subscribe(v => {

            this.groupService.autocomplete(v).subscribe(d => {
                this.filteredGroups = d;
            });
        });
    }

    displayGroupFn(param: Group): string | undefined {
        if (!param) return undefined;
        return param.name;
    }

    addLesson() {
        this.router.navigate(['/dashboard/lms/courses/lesson/' + this.id + '/0']);
    }

    addGroup() {
        const entry = this.dataForm.controls['group'].value;
        this.groupsGridApi.updateRowData({ add: [entry] });
    }

    getCourse(): CourseDetails {
        const course = this.dataForm.value as CourseDetails;
        course.groups = this.getGroups();
        course.id = this.id;
        if (course.passThreshold !== null) {
            course.passThreshold = course.passThreshold / 100.0;
        }

        return course;
    }

    onSubmit() {
        const course = this.getCourse();
        if (this.id === 0) {
            this.store.dispatch(new CourseActions.AddCourse(course));
        }
        else {
            this.store.dispatch(new CourseActions.UpdateCourse(course));
        }
    }

    private getGroups(): CourseGroup[] {

        const nodes = [];
        this.groupsGridApi.forEachNode((node, i) => {
            const data = <CourseGroup>node.data;

            const course = {
                ...data,
                fromDate: toISODateString(data.fromDate),
                toDate: toISODateString(data.toDate)
            };

            nodes.push(course);
        });

        return nodes;
    }

    getRowNodeId(node: any) {
        return node.id;
    }

    back() {
        this.router.navigate(['dashboard', 'lms', 'courses']);
    }

    navigateToReports() {
        this.router.navigate(['dashboard', 'lms', 'reports', this.id]);
    }

}

function formatFromDate(val) {
    const date = val.data as CourseGroup;
    if (date && date.fromDate) {
        return toLocalDateString(date.fromDate);
    }
    return '';
}


function formatToDate(val) {
    const date = val.data as CourseGroup;
    if (date && date.toDate) {
        return toLocalDateString(date.toDate);
    }
    return '';
}




