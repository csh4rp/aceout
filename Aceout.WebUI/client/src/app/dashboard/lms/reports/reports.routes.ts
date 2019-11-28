import { Routes } from '@angular/router';
import { CourseReportsComponent } from './course-reports/course-reports.component';
import { ReportsComponent } from './reports.component';
import { LessonReportsComponent } from './lesson-reports/lesson-reports.component';

export const routes: Routes = [
    {
        path: '', component: ReportsComponent,
        children: [
            { path: ':courseId', component: CourseReportsComponent },
            { path: ':courseId/:lessonId', component: LessonReportsComponent }
        ]
    },
];
