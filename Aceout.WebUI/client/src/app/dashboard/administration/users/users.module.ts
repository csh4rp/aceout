import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AgGridModule } from 'ag-grid-angular';

import {UsersMainComponent} from './main/users.main.component';
import { TranslateModule } from '@ngx-translate/core';

import { routes } from './users.routes';
import { UsersComponent } from './users.component';
import { UsersEditComponent } from './edit/users.edit.component';
import { FontAwesomeModule} from '@fortawesome/angular-fontawesome';
import {library} from '@fortawesome/fontawesome-svg-core';
import { fas } from '@fortawesome/free-solid-svg-icons';
import { far } from '@fortawesome/free-regular-svg-icons';
import { SharedDashboardModule } from 'src/app/shared-dashboard';
import {UsersStoreModule} from '../../../store/dashboard/administration/users/users.store.module'; 
import { RolesStoreModule } from 'src/app/store/dashboard/administration/roles/roles.store.module';
import { CommonStoreModule } from 'src/app/store/dashboard/common/common.store.module';
import { MatInput, MatInputModule, MatCheckboxModule } from '@angular/material';

library.add(fas, far);

@NgModule({
  declarations: [
    UsersMainComponent,
    UsersEditComponent,
    UsersComponent
  ],
  imports: [
    TranslateModule,
    AgGridModule.withComponents([]),
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatInputModule,
    MatCheckboxModule,
    RouterModule.forChild(routes),
    FontAwesomeModule,
    SharedDashboardModule,
    UsersStoreModule,
    RolesStoreModule,
    CommonStoreModule
  ],
})
export class UsersModule {
  public static routes = routes;
}
