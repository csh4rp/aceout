import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';
import { InformationsStoreModule } from 'src/app/store/dashboard/cms/informations/informations.store.module';
import { routes } from './user-informations.routes';
import { UserInformationsComponent } from './user-informations.component';

@NgModule({
  declarations: [
    UserInformationsComponent
  ],
  imports: [
    TranslateModule,
    CommonModule,
    RouterModule.forChild(routes),
    InformationsStoreModule
  ],
  exports: [
    TranslateModule,
    UserInformationsComponent
  ]
})
export class UserInformationsModule {
  public static routes = routes;
}
