import { Routes } from '@angular/router';

export const routes: Routes = [
  { path: '', loadChildren: './categories#CategoriesModule' },
  { path: 'categories', loadChildren: './categories#CategoriesModule' },
  { path: 'materials', loadChildren: './materials#MaterialsModule' },
  { path: 'course-paths', loadChildren: './course-paths#CoursePathsModule' },
  { path: 'courses', loadChildren: './courses#CoursesModule' },
  { path: 'reports', loadChildren: './reports#ReportsModule' }
];
