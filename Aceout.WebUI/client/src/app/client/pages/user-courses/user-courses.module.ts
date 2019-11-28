import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';
import { InformationsStoreModule } from 'src/app/store/dashboard/cms/informations/informations.store.module';
import { routes } from './user-courses.routes';
import { UserCoursesComponent } from './user-courses.component';
import { UserCoursesStoreModule } from 'src/app/store/client/user-courses/user-courses.store.module';
import { ControlsModule } from '../../controls/controls.module';


@NgModule({
  declarations: [
    UserCoursesComponent
  ],
  imports: [
    TranslateModule,
    CommonModule,
    RouterModule.forChild(routes),
    UserCoursesStoreModule,
    ControlsModule
  ],
  exports: [
    TranslateModule,
    UserCoursesComponent
  ]
})
export class UserCoursesModule {
  public static routes = routes;
}
