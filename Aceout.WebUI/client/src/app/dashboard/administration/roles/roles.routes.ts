import { Routes } from '@angular/router';
import { RolesComponent } from './roles.component';
import { RolesMainComponent } from './main/roles.main.component';
import { RolesEditComponent } from './edit/roles.edit.component';

export const routes: Routes = [
  {
    path: '', component: RolesComponent,
    children: [
      { path: '', component: RolesMainComponent },
      { path: 'main', component: RolesMainComponent },
      { path: 'edit/:id', component: RolesEditComponent }
    ]
  },

];
