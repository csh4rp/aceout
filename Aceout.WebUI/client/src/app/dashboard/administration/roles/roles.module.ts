import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AgGridModule } from 'ag-grid-angular';
import { TranslateModule } from '@ngx-translate/core';

import { routes } from './roles.routes';
import { FontAwesomeModule} from '@fortawesome/angular-fontawesome';
import {library} from '@fortawesome/fontawesome-svg-core';
import { fas } from '@fortawesome/free-solid-svg-icons';
import { far } from '@fortawesome/free-regular-svg-icons';
import { SharedDashboardModule } from 'src/app/shared-dashboard';
import { RolesMainComponent } from './main/roles.main.component';
import { RolesComponent } from './roles.component';
import { RolesEditComponent } from './edit/roles.edit.component';
import { RolesStoreModule } from 'src/app/store/dashboard/administration/roles/roles.store.module';
import { CommonStoreModule } from 'src/app/store/dashboard/common/common.store.module';

library.add(fas, far);

@NgModule({
  declarations: [
    RolesMainComponent,
    RolesEditComponent,
    RolesComponent
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
    RolesStoreModule,
    CommonStoreModule
  ],
})
export class RolesModule {
  public static routes = routes;
}
