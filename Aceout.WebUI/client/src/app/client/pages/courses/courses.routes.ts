import { Routes } from '@angular/router';

import { CourseDetailsComponent } from './course-details/course-details.component';
import { CoursesComponent } from './courses.component';
import { ElementDetailsComponent } from './element-details/element-details.component';
import { LessonDetailsComponent } from './lesson-details/lesson-details.component';


export const routes: Routes = [
  {
    path: '', component: CoursesComponent,
    children: [
      { path: ':id', component: CourseDetailsComponent, },
      { path: ':id/:lessonId', component: LessonDetailsComponent },
      { path: ':id/:lessonId/:position', component: ElementDetailsComponent },
    ]
  },

];
