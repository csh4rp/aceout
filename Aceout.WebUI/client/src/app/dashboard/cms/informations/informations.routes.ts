import { Routes } from '@angular/router';
import { InformationsComponent } from './informations.component';
import { InformationsMainComponent } from './main/informations.main.component';
import { InformationsEditComponent } from './edit/informations.edit.component';

export const routes: Routes = [
  {
    path: '', component: InformationsComponent,
    children: [
      { path: '', component: InformationsMainComponent },
      { path: 'edit/:id', component: InformationsEditComponent }
    ]
  },

];
