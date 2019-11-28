import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { routes } from './dashboard.routes';
import { DashboardComponent } from './dashboard.component';
import { SharedDashboardModule } from '../shared-dashboard';

import { FontAwesomeModule} from '@fortawesome/angular-fontawesome';
import {library} from '@fortawesome/fontawesome-svg-core';
import { fas } from '@fortawesome/free-solid-svg-icons';
import { far } from '@fortawesome/free-regular-svg-icons';
import { MatDialogModule } from '@angular/material';

library.add(fas, far);

@NgModule({
  declarations: [
    DashboardComponent
  ],
  imports: [
    SharedDashboardModule,
    CommonModule,
    FormsModule,
    RouterModule.forChild(routes),
    FontAwesomeModule,
    MatDialogModule
  ],
  exports: [
    MatDialogModule
  ]
})
export class DashboardModule {
  public static routes = routes;
}
