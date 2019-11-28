import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AgGridModule } from 'ag-grid-angular';


import { TranslateModule } from '@ngx-translate/core';


import { FontAwesomeModule} from '@fortawesome/angular-fontawesome';
import {library} from '@fortawesome/fontawesome-svg-core';
import { fas } from '@fortawesome/free-solid-svg-icons';
import { far } from '@fortawesome/free-regular-svg-icons';
import { SharedDashboardModule } from 'src/app/shared-dashboard';
import { GroupsComponent } from './groups.component';
import { routes } from './groups.routes';
import { GroupsMainComponent } from './main/groups.main.component';
import { GroupsEditComponent } from './edit/groups.edit.component';
import { GroupsStoreModule } from 'src/app/store/dashboard/organization/groups/groups.store.module';

library.add(fas, far);

@NgModule({
  declarations: [
    GroupsComponent,
    GroupsMainComponent,
    GroupsEditComponent
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
    GroupsStoreModule
  ],
})
export class GroupsModule {
  public static routes = routes;
}
