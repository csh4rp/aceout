
import * as fromCourses from '../reducers';
import { createSelector } from '@ngrx/store';
import { CoursesState } from '../reducers/courses.reducer';

export const getCoursesState = createSelector(fromCourses.getCoursesRootState, (s : fromCourses.CoursesRootState) => s ? s.courses : undefined);
export const getCourse = createSelector(getCoursesState, (s : CoursesState) => s ? s.course : undefined);
export const getCoursesLoaded = createSelector(getCoursesState, (s: CoursesState) => s ? s.isLoaded : undefined);
