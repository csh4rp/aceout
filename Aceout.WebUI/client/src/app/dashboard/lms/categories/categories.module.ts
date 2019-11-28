import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AgGridModule } from 'ag-grid-angular';

import { TranslateModule } from '@ngx-translate/core';

import { routes } from './categories.routes';
import { CategoriesComponent } from './categories.component';
import { CategoriesMainComponent } from './main/categories.main.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { SharedDashboardModule } from 'src/app/shared-dashboard';
import { CategoriesEditComponent } from './edit/categories.edit.component';
import { MaterialCategoriesStoreModule } from 'src/app/store/dashboard/lms/material-categories/material-categories.store.module';
import { MatInputModule } from '@angular/material';
import { CommonStoreModule } from 'src/app/store/dashboard/common/common.store.module';



@NgModule({
  declarations: [
    CategoriesComponent,
    CategoriesMainComponent,
    CategoriesEditComponent
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
    MatInputModule,
    MaterialCategoriesStoreModule,
    CommonStoreModule
  ],
})
export class CategoriesModule {
  public static routes = routes;
}
