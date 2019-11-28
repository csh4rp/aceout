import { Component, OnInit, ViewEncapsulation, Inject } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { HttpClient } from '@angular/common/http';
import { UrlHelper } from 'src/app/app.urls';
import { ButtonRowRenderer } from 'src/app/shared-dashboard/grid/buttons/button-row-renderer.component';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/dashboard/administration/user.service';
import { GridDataSource } from 'src/app/model/gridDataSource';
import { GridControl } from 'src/app/controls/gridControl';
import { SortModel } from 'src/app/model/sortModel';
import { MatDialog, MatSnackBar, MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { MaterialService } from 'src/app/services/dashboard/lms/material.service';
import { FormGroup, FormControl, FormGroupDirective } from '@angular/forms';
import { LessonElement } from 'src/app/model/dashboard/lms/lessons/lesson-element';
import { Lesson } from 'src/app/model/dashboard/lms/lessons/lesson';

@Component({
    templateUrl: './elements.component.html'
})
export class ElementsComponent extends GridControl implements OnInit {

    frameworkComponents: any;
    gridApi: any;
    columns: any[];

    constructor(translate: TranslateService,
        private materialService: MaterialService,
        private router: Router,
        @Inject(MAT_DIALOG_DATA) public data: LessonElement) {
        super(translate);

    }

    ngOnInit() {
        this.columns = [
            { colId: 'Name', headerName: 'name', field: 'name' }
        ];

        this.frameworkComponents = {
            buttonRowRenderer: ButtonRowRenderer
        };
    }

    redirect(param: any) {
        return '/dashboard/lms/course-paths/edit/' + param;
    }


    gridReady(params) {
        this.gridApi = params.api;
        super.onGridReady(params, this.columns);
        const ds = new GridDataSource(this.materialService, new SortModel("id", "asc"));
        this.gridApi.setDatasource(ds);
        this.gridApi.sizeColumnsToFit();
    }

    changeDs() {
        const ds = new GridDataSource(this.materialService, new SortModel("id", "asc"));
        this.gridApi.setDatasource(ds);
    }

    add() {
        this.router.navigate([this.redirect(0)]);
    }

    onSelectionChanged() {
        const selectedRows = this.gridApi.getSelectedRows();
        const row = selectedRows[0];
        const element = new LessonElement();
        element.materialId = row.id;
        element.materialName = row.name;
        element.scale = 1;

        this.data = element;
    }
}

