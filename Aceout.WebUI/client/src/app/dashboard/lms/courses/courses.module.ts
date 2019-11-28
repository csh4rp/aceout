import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AgGridModule } from 'ag-grid-angular';

import { TranslateModule } from '@ngx-translate/core';

import { routes } from './courses.routes';
import { CoursesComponent } from './courses.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { SharedDashboardModule } from 'src/app/shared-dashboard';
import { CoursesEditComponent } from './edit/courses.edit.component';
import { CoursesMainComponent } from './main/courses.main.component';
import { LessonsEditComponent } from './lesson/lessons.edit.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CoursesStoreModule } from 'src/app/store/dashboard/lms/courses/courses.store.module';
import { CoursePathsStoreModule } from 'src/app/store/dashboard/lms/course-paths/course-paths.store.module';
import { GroupsStoreModule } from 'src/app/store/dashboard/organization/groups/groups.store.module';
import { LessonsStoreModule } from 'src/app/store/dashboard/lms/lessons/lessons.store.module';
import { MatExpansionModule } from '@angular/material';
import { CommonStoreModule } from 'src/app/store/dashboard/common/common.store.module';



@NgModule({
  declarations: [
    CoursesComponent,
    CoursesMainComponent,
    CoursesEditComponent,
    LessonsEditComponent
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
    CoursesStoreModule,
    CoursePathsStoreModule,
    GroupsStoreModule,
    LessonsStoreModule,
    CommonStoreModule,
    MatExpansionModule
  ],
})
export class CoursesModule {
  public static routes = routes;
}
