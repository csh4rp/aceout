import { Routes } from '@angular/router';
import { CoursePathComponent } from './course-paths.component';
import { CoursePathsMainComponent } from './main/course-paths.main.component';
import { CoursePathsEditComponent } from './edit/course-paths.edit.component';

export const routes: Routes = [
  {
    path: '', component: CoursePathComponent,
    children: [
      { path: '', component: CoursePathsMainComponent },
      { path: 'edit/:id', component: CoursePathsEditComponent }
    ]
  },

];
