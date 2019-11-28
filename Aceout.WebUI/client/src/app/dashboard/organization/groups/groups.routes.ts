import { Routes } from '@angular/router';
import { GroupsComponent } from './groups.component';
import { GroupsMainComponent } from './main/groups.main.component';
import { GroupsEditComponent } from './edit/groups.edit.component';

export const routes: Routes = [
  {
    path: '', component: GroupsComponent,
    children: [
      { path: '', component: GroupsMainComponent },
      { path: 'edit/:id', component: GroupsEditComponent }
    ]
  },

];
