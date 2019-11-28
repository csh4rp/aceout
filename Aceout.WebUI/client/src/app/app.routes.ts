import { Routes } from '@angular/router';
import { LoginComponent } from './login';
import { AuthGuardService } from './services';
import { RegisterComponent } from './register';
import { FilesComponent } from './shared-dashboard/files/files.component';

export const ROUTES: Routes = [
    { path: '', loadChildren: './client#ClientModule', },
    { path: 'dashboard', loadChildren: './dashboard#DashboardModule', canActivate: [AuthGuardService] },
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'files/browse', component: FilesComponent }
];
