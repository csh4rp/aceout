import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AgGridModule } from 'ag-grid-angular';

import { TranslateModule } from '@ngx-translate/core';

import { routes } from './reports.routes';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { SharedDashboardModule } from 'src/app/shared-dashboard';
import { MatExpansionModule } from '@angular/material';
import { CourseReportsComponent } from './course-reports/course-reports.component';
import { ReportsComponent } from './reports.component';
import { LessonReportsComponent } from './lesson-reports/lesson-reports.component';



@NgModule({
    declarations: [
        ReportsComponent,
        CourseReportsComponent,
        LessonReportsComponent
    ],
    imports: [
        TranslateModule,
        AgGridModule.withComponents([]),
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule.forChild(routes),
        FontAwesomeModule,
        SharedDashboardModule,
        MatExpansionModule
    ],
})
export class ReportsModule {
    public static routes = routes;
}
