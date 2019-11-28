import { Routes } from '@angular/router';


export const routes: Routes = [
  { path: '', loadChildren: './users#UsersModule' },
  { path: 'users', loadChildren: './users#UsersModule' },
  { path: 'roles', loadChildren: './roles#RolesModule' }
];
