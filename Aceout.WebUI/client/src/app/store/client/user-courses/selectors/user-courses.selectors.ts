
import * as fromCourses from '../reducers';
import { createSelector } from '@ngrx/store';


export const getCoursesState = createSelector(fromCourses.getCoursesRootState, s => s.courses);
export const getCourses = createSelector(getCoursesState, s => s.courses);
export const getSelectedCourse = createSelector(getCoursesState, s => s.selectedCourse);
