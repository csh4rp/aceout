import { Routes } from '@angular/router';
import { UsersMainComponent } from './main/users.main.component';
import { UsersComponent } from './users.component';
import { UsersEditComponent } from './edit/users.edit.component';

export const routes: Routes = [
  {
    path: '', component: UsersComponent,
    children: [
      { path: '', component: UsersMainComponent },
      { path: 'main', component: UsersMainComponent },
      { path: 'edit/:id', component: UsersEditComponent }
    ]
  },

];
