import { Routes } from '@angular/router';
import { ClientComponent } from './client.component';
import { HomeComponent } from './pages/home/home.component';
import { InformationsComponent } from '../dashboard/cms/informations/informations.component';


export const routes: Routes = [
    {
        path: '', component: ClientComponent,
        children: [
            { path: '', component: HomeComponent },
            { path: 'informations', loadChildren: './pages/user-informations#UserInformationsModule' },
            { path: 'courses', loadChildren: './pages/courses#CoursesModule' },
            { path: 'my-courses', loadChildren: './pages/user-courses#UserCoursesModule' },
            { path: 'course-paths', loadChildren: './pages/user-paths#UserPathsModule' }
        ]
    },

];
