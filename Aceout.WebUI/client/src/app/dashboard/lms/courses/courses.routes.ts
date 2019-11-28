import { Routes } from '@angular/router';
import { CoursesComponent } from './courses.component';
import { CoursesMainComponent } from './main/courses.main.component';
import { CoursesEditComponent } from './edit/courses.edit.component';
import { LessonsEditComponent } from './lesson/lessons.edit.component';

export const routes: Routes = [
  {
    path: '', component: CoursesComponent,
    children: [
      { path: '', component: CoursesMainComponent },
      { path: 'lesson/:courseId/:lessonId', component: LessonsEditComponent },
      { path: 'edit/:id', component: CoursesEditComponent },

    ]
  },

];
