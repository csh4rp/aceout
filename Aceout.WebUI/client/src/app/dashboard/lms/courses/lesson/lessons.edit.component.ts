import { Component, OnInit } from '@angular/core';
import { ButtonRowRenderer } from 'src/app/shared-dashboard/grid/buttons/button-row-renderer.component';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl, Validators, FormGroupDirective, NgForm } from '@angular/forms';
import { ShowOnDirtyErrorStateMatcher, MatSnackBar, MatDialog } from '@angular/material';
import { ElementsComponent } from '../../elements/elements.component';
import { CheckboxRowRenderer } from 'src/app/shared-dashboard/grid/checkbox/checkbox-row-renderer.component';
import { KeyValuePair } from 'src/app/model/framework/key-value-pair';
import { LessonType } from 'src/app/model/dashboard/lms/lessons/lesson-type';
import { Lesson } from 'src/app/store/dashboard/lms/lessons/models/lesson.model';
import { LessonElement } from 'src/app/store/dashboard/lms/lessons/models/lesson-element.model';
import { assignFormValues } from 'src/app/helpers/forms';
import { Observable } from 'rxjs';
import { Store } from '@ngrx/store';
import { AppState } from 'src/app/store/app.state';
import { getLesson } from 'src/app/store/dashboard/lms/lessons/selectors/lessons.selectors';
import { getNumericCellEditor } from 'src/app/helpers/native-controls';
import * as LessonActions from 'src/app/store/dashboard/lms/lessons/actions';
import { EditorSettings } from 'src/app/model/framework/editor-settings.model';
import { GridControl } from 'src/app/controls/gridControl';
import { TranslateService } from '@ngx-translate/core';

@Component({
    templateUrl: './lessons.edit.component.html'
})
export class LessonsEditComponent extends GridControl implements OnInit {

    form: FormGroup;
    buttons: any[];
    columns: any[];
    lesson$: Observable<Lesson>;
    settings: any;
    titleText: string;
    submitText: string;

    lessonTypes: KeyValuePair<number, string>[];

    private id: number;
    private courseId: number;
    private gridApi: any;
    private gridColumnApi: any;
    private components: any;
    private rowData: LessonElement[];

    constructor(private translateService: TranslateService,
        private router: Router,
        private route: ActivatedRoute,
        private dialog: MatDialog,
        private store: Store<AppState>) {
        super(translateService);
    }

    public frameworkComponents = {
        buttonRowRenderer: ButtonRowRenderer,
        checkboxRowRenderer: CheckboxRowRenderer
    };

    openDialog() {
        const dialogDef = this.dialog.open(ElementsComponent, {
            width: '600px'
        });

        dialogDef.afterClosed().subscribe(d => {
            this.gridApi.updateRowData({ add: [d] });
        })
    }

    ngOnInit(): void {

        this.form = new FormGroup({
            name: new FormControl('', Validators.required),
            description: new FormControl('', []),
            type: new FormControl(0, []),
            attemptCount: new FormControl(null, []),
            passThreshold: new FormControl(null, []),
            isActive: new FormControl(false, []),
            allowAnswerCheck: new FormControl(false, []),
            allowAnswerPreview: new FormControl(false, [])
        });

        this.settings = EditorSettings;

        this.buttons = [
            {
                name: 'delete',
                action: p => {
                    let node = null;
                    this.gridApi.forEachNode((n, i) => {
                        if (n.data.materialId == p) {
                            node = n.data;
                            return;
                        }
                    });
                    this.gridApi.updateRowData({ remove: [node] });
                },
                icon: 'trash',
                data: 'materialId'
            }
        ];

        this.columns = [
            { colId: 'Name', headerName: 'Name', field: 'materialName' },
            { colId: 'Scale', headerName: 'Scale', field: 'scale', editable: true, cellEditor: "numericCellEditor" },
            {
                colId: 'Is active', cellRenderer: 'checkboxRowRenderer', headerName: 'Is Active', field: 'materialId',
                width: 90, suppressSizeToFit: true,
                cellRendererParams: { dataKey: 'materialId', checkedProp: 'isActive' }
            },
            {
                colId: 'button', cellRenderer: 'buttonRowRenderer', headerName: '', field: 'id',
                width: 90, suppressSizeToFit: true,
                cellRendererParams: { buttons: this.buttons }
            }
        ];

        this.components = { numericCellEditor: getNumericCellEditor() };

        this.route.params.subscribe(p => {
            this.id = parseInt(p['lessonId']);
            this.courseId = parseInt(p['courseId']);

            if (this.id > 0) {

                this.submitText = 'Save changes';
                this.titleText = 'Edit lesson';

                this.lesson$ = this.store.select(getLesson);
                this.store.dispatch(new LessonActions.GetLesson(this.id));

                this.lesson$.subscribe(lesson => {
                    if (lesson) {
                        assignFormValues(lesson, this.form);

                        if(lesson.passThreshold !== null){
                            this.form.controls['passThreshold'].setValue(lesson.passThreshold * 100.0);
                        }

                        this.rowData = lesson.elements;
                        if (this.gridApi) {
                            this.gridApi.setRowData(this.rowData);
                        }
                    }
                });
            }
            else {
                this.submitText = 'Add';
                this.titleText = 'Add lesson';
            }
        });


        this.lessonTypes = [];
        for (let type in LessonType) {
            if (!isNaN(Number(type))) {
                this.lessonTypes.push(new KeyValuePair(Number(type), LessonType[type]));
            }
        }

    }

    gridReady(params) {
        super.onGridReady(params, this.columns);
        this.gridApi = params.api;
        this.gridColumnApi = params.columnApi;
        this.gridApi.setRowData(this.rowData);
    }

    onSubmit() {

        const lesson = this.form.value as Lesson;
        lesson.id = this.id;
        lesson.courseId = this.courseId;
        lesson.elements = [];

        if (lesson.passThreshold !== null) {
            lesson.passThreshold = lesson.passThreshold / 100.0;
        }

        this.gridApi.forEachNode(node => {
            lesson.elements.push(node.data as LessonElement);
        });

        if (this.id === 0) {
            this.store.dispatch(new LessonActions.AddLesson(lesson));
        }
        else {
            this.store.dispatch(new LessonActions.UpdateLesson(lesson));
        }
    }

    back() {
        this.router.navigate(['dashboard', 'lms', 'courses', 'edit', this.courseId]);
    }

}

