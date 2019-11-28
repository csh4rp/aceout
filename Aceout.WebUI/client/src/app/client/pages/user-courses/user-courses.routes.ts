import { Routes } from '@angular/router';
import { UserCoursesComponent } from './user-courses.component';


export const routes: Routes = [
    {
        path: '', component: UserCoursesComponent
    },
    {
        path: ':coursePathId', component: UserCoursesComponent
    }

];
