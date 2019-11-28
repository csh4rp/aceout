import { Routes } from '@angular/router';
import { MaterialsComponent } from './materials.component';
import { MaterialsMainComponent } from './main/materials.main.component';
import { MaterialsEditComponent } from './edit/materials.edit.component';

export const routes: Routes = [
  {
    path: '', component: MaterialsComponent,
    children: [
      { path: '', component: MaterialsMainComponent },
      { path: 'edit/:id', component: MaterialsEditComponent }
    ]
  },

];
