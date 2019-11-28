import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';
import { InformationsStoreModule } from 'src/app/store/dashboard/cms/informations/informations.store.module';
import { routes } from './user-paths.routes';
import { UserCoursesStoreModule } from 'src/app/store/client/user-courses/user-courses.store.module';
import { ControlsModule } from '../../controls/controls.module';
import { UserPathsComponent } from './user-paths.component';
import { CoursePathsModule } from 'src/app/dashboard/lms/course-paths';
import { CoursePathsStoreModule } from 'src/app/store/dashboard/lms/course-paths/course-paths.store.module';


@NgModule({
  declarations: [
    UserPathsComponent
  ],
  imports: [
    TranslateModule,
    CommonModule,
    RouterModule.forChild(routes),
    CoursePathsStoreModule,
    ControlsModule
  ],
  exports: [
    TranslateModule,
    UserPathsComponent
  ]
})
export class UserPathsModule {
  public static routes = routes;
}
