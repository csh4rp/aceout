import { Routes } from '@angular/router';
import { CategoriesComponent } from './categories.component';
import { CategoriesMainComponent } from './main/categories.main.component';
import { CategoriesEditComponent } from './edit/categories.edit.component';

export const routes: Routes = [
  {
    path: '', component: CategoriesComponent,
    children: [
      { path: '', component: CategoriesMainComponent },
      { path: 'edit/:id', component: CategoriesEditComponent }
    ]
  },

];
