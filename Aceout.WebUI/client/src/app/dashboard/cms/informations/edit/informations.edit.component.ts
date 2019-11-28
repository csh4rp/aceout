import { Component, OnInit, ViewEncapsulation, NgZone } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { HttpClient } from '@angular/common/http';
import { UrlHelper } from 'src/app/app.urls';
import { ButtonRowRenderer } from 'src/app/shared-dashboard/grid/buttons/button-row-renderer.component';
import { Router, ActivatedRoute } from '@angular/router';
import { GridControl } from 'src/app/controls/gridControl';
import { FormGroup, FormControl, Validators, FormGroupDirective, NgForm } from '@angular/forms';
import { ErrorStateMatcher, ShowOnDirtyErrorStateMatcher, MatSnackBar } from '@angular/material';
import { GroupService } from 'src/app/services/dashboard/organization/group.service';
import { Store } from '@ngrx/store';
import { AppState } from 'src/app/store/app.state';
import * as  InfoActions from 'src/app/store/dashboard/cms/informations/actions';
import { Observable } from 'rxjs';
import { Group } from 'src/app/store/dashboard/organization/groups/models/group.model';
import { CourseGroup } from 'src/app/store/dashboard/lms/courses/models/course-group.model';
import { formatHelper } from 'src/app/helpers/formatHelper';
import { NumericEditor } from 'src/app/shared-dashboard/grid/numeric-editor/numeric-editor.component';
import * as moment from 'moment';
import { toLocalDateString, toISODateString } from 'src/app/helpers/dates';
import { getDatePicker } from 'src/app/helpers/native-controls';
import { assignFormValues } from 'src/app/helpers/forms';
import { EditorSettings } from 'src/app/model/framework/editor-settings.model';
import { InformationViewModel } from 'src/app/store/dashboard/cms/informations/models/information.view-model';
import { getInformation } from 'src/app/store/dashboard/cms/informations/selectors/informations.selectors';
import { InformationDetails } from 'src/app/store/dashboard/cms/informations/models/information-details.model';
import { GroupInformation } from 'src/app/store/dashboard/cms/informations/models/group-information.model';
import { debounceTime, switchMap } from 'rxjs/operators';
declare var $
declare var elFinderBrowser;

@Component({
    templateUrl: './informations.edit.component.html'
})
export class InformationsEditComponent extends GridControl implements OnInit {


    dataForm: FormGroup;
    titleText: string;
    submitText: string;
    buttons: any[];
    groupButtons: any[];
    groupColumns: any[];
    frameworkComponents: any;
    information$: Observable<InformationViewModel>;

    settings = EditorSettings;

    private id: number;
    private groupsGridApi;
    private groupsGridColumnApi;
    groups: GroupInformation[];
    filteredGroups: Group[];

    constructor(translate: TranslateService,
        private groupService: GroupService,
        private route: ActivatedRoute,
        private router: Router,
        private zone: NgZone,
        private store: Store<AppState>) {
        super(translate);
    }

    onGroupsGridReady(params) {
        super.onGridReady(params, this.groupColumns);
        this.groupsGridApi = params.api;
        this.groupsGridColumnApi = params.columnApi;

        this.groupsGridApi.setRowData(this.groups);
        params.api.sizeColumnsToFit();
    }

    ngOnInit() {

        this.dataForm = new FormGroup({
            title: new FormControl('', Validators.required),
            content: new FormControl('', []),
            fromDate: new FormControl('', []),
            toDate: new FormControl('', []),
            group: new FormControl('', [])
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
            {
                colId: 'button', cellRenderer: 'buttonRowRenderer', headerName: '', field: 'id',
                width: 90, suppressSizeToFit: true,
                cellRendererParams: { buttons: this.groupButtons }
            }
        ];

        this.frameworkComponents = {
            buttonRowRenderer: ButtonRowRenderer,
            numericEditor: NumericEditor
        };

        this.route.params.subscribe(p => {
            this.id = parseInt(p['id']);

            if (this.id > 0) {
                this.titleText = 'Edit information';
                this.submitText = 'Save changes';

                this.information$ = this.store.select(getInformation);
                this.information$.subscribe(info => {
                    if (info) {
                        assignFormValues(info, this.dataForm);
                        this.groups = info.groups;

                        if (this.groupsGridApi) {
                            this.groupsGridApi.setRowData(this.groups);
                        }
                    }
                });

                this.store.dispatch(new InfoActions.GetInformation(this.id));
            }
            else {
                this.titleText = 'Add information';
                this.submitText = 'Add';
            }
        });

        this.dataForm.controls['group'].valueChanges.pipe(debounceTime(500),
            switchMap((val) =>
                this.groupService.autocomplete(val),
            )).subscribe(val => {
                this.filteredGroups = val;
            });


    }

    displayGroupFn(param: Group): string | undefined {
        if (!param) return undefined;
        return param.name;
    }

    addGroup() {
        const entry = this.dataForm.controls['group'].value;
        this.groupsGridApi.updateRowData({ add: [entry] });
    }

    getInformation(): InformationDetails {
        const info = this.dataForm.value as InformationDetails;
        info.groupIds = this.getGroups();
        info.id = this.id;
        info.fromDate = formatHelper.toISODate(info.fromDate);


        return info;
    }

    onSubmit() {
        const info = this.getInformation();
        if (this.id === 0) {
            this.store.dispatch(new InfoActions.AddInformation(info));
        }
        else {
            this.store.dispatch(new InfoActions.UpdateInformation(info));
        }
    }

    private getGroups(): number[] {

        const nodes = [];
        this.groupsGridApi.forEachNode(node => {
            nodes.push(node.data.id);
        });

        return nodes;
    }

    getRowNodeId(node: any) {
        return node.id;
    }

    back() {
        this.router.navigate(['dashboard', 'cms', 'informations']);
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




