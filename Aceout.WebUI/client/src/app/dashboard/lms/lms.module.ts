import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AgGridModule } from 'ag-grid-angular';

import { TranslateModule } from '@ngx-translate/core';

import { routes } from './lms.routes';
import { LmsComponent } from './lms.component';
import { MaterialsComponent } from './materials/materials.component';
import { ElementsComponent } from './elements/elements.component';
import { MatDialogModule } from '@angular/material';
import { SharedDashboardModule } from 'src/app/shared-dashboard';



@NgModule({
  declarations: [
    LmsComponent,
    ElementsComponent
  ],
  imports: [
    TranslateModule,
    AgGridModule.withComponents([]),
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    SharedDashboardModule,
    RouterModule.forChild(routes),
    MatDialogModule
  ],
  entryComponents:[
    ElementsComponent
  ]
})
export class LmsModule {
  public static routes = routes;
}
