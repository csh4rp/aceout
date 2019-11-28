import { Routes } from '@angular/router';
import { DashboardComponent } from './dashboard.component';

export const routes: Routes = [
  {
    path: '', component: DashboardComponent,
    children: [
      { path: 'administration', loadChildren: './administration#AdministrationModule' },
      { path: 'lms', loadChildren: './lms#LmsModule' },
      { path: 'organization', loadChildren: './organization#OrganizationModule' },
      { path: 'cms', loadChildren: './cms#CmsModule' }
    ]
  },

];
