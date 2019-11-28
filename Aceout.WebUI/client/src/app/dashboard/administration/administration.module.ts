import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { routes } from './administration.routes';
import { AdministrationComponent } from './administration.component';


@NgModule({
  declarations: [
    AdministrationComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    RouterModule.forChild(routes),
  ],
})
export class AdministrationModule {
  public static routes = routes;
}
