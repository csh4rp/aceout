import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AgGridModule } from 'ag-grid-angular';

import { TranslateModule } from '@ngx-translate/core';

import { routes } from './materials.routes';
import { MaterialsComponent } from './materials.component';
import { MaterialsMainComponent } from './main/materials.main.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { SharedDashboardModule } from 'src/app/shared-dashboard';
import { MaterialsEditComponent } from './edit/materials.edit.component';
import { SingleAnswerComponent } from '../templates/single-answer/single-answer.component';
import { MaterialTemplate } from '../templates/material-template.directive';
import { MaterialsStoreModule } from 'src/app/store/dashboard/lms/materials/materials.store.module';
import { MaterialCategoriesStoreModule } from 'src/app/store/dashboard/lms/material-categories/material-categories.store.module';
import { MatExpansionModule } from '@angular/material';
import { CommonStoreModule } from 'src/app/store/dashboard/common/common.store.module';



@NgModule({
  declarations: [
    MaterialsComponent,
    MaterialsMainComponent,
    MaterialsEditComponent,
    SingleAnswerComponent,
    MaterialTemplate
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
    MaterialsStoreModule,
    MatExpansionModule,
    MaterialCategoriesStoreModule,
    CommonStoreModule
  ],
  entryComponents:[
    SingleAnswerComponent
  ]
})
export class MaterialsModule {
  public static routes = routes;
}
