import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AgGridModule } from 'ag-grid-angular';

import { TranslateModule } from '@ngx-translate/core';

import { routes } from './course-paths.routes';
import { CoursePathComponent } from './course-paths.component';
import { CoursePathsMainComponent } from './main/course-paths.main.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { SharedDashboardModule } from 'src/app/shared-dashboard';
import { CoursePathsEditComponent } from './edit/course-paths.edit.component';
import { CoursePathsStoreModule } from 'src/app/store/dashboard/lms/course-paths/course-paths.store.module';
import { CommonStoreModule } from 'src/app/store/dashboard/common/common.store.module';



@NgModule({
  declarations: [
    CoursePathComponent,
    CoursePathsMainComponent,
    CoursePathsEditComponent
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
    CoursePathsStoreModule,
    CommonStoreModule
  ],
})
export class CoursePathsModule {
  public static routes = routes;
}
