import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { SharedDashboardModule } from '../shared-dashboard';

import { FontAwesomeModule} from '@fortawesome/angular-fontawesome';
import {library} from '@fortawesome/fontawesome-svg-core';
import { fas } from '@fortawesome/free-solid-svg-icons';
import { far } from '@fortawesome/free-regular-svg-icons';
import { MatDialogModule } from '@angular/material';
import { ClientComponent } from './client.component';
import { routes } from './client.routes';
import { WidgetsModule } from './widgets/widgets.module';
import { TranslateModule } from '@ngx-translate/core';
import { ControlsModule } from './controls/controls.module';
import { HomeComponent } from './pages/home/home.component';



@NgModule({
  declarations: [
    ClientComponent,
    HomeComponent
  ],
  imports: [
    TranslateModule,
    WidgetsModule,
    ControlsModule,
    CommonModule,
    RouterModule.forChild(routes),
    MatDialogModule
  ],
  exports: [
    TranslateModule,
    HomeComponent
  ]
})
export class ClientModule {
  public static routes = routes;
}
