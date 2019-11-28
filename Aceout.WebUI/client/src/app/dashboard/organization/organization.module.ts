import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { routes } from './organization.routes';
import { OrganizationComponent } from './organization.component';


@NgModule({
  declarations: [
    OrganizationComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    RouterModule.forChild(routes),
  ],
})
export class OrganizationModule {
  public static routes = routes;
}
