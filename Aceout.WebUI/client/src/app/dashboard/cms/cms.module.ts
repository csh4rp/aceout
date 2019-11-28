import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { routes } from './cms.routes';
import { CmsComponent } from './cms.component';


@NgModule({
  declarations: [
    CmsComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    RouterModule.forChild(routes),
  ],
})
export class CmsModule {
  public static routes = routes;
}
