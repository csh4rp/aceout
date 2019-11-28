import * as fromCourses from './courses.reducer';
import { ActionReducerMap, createFeatureSelector } from '@ngrx/store';



export interface CoursesRootState {
    courses: fromCourses.CoursesState;
};

export const reducers : ActionReducerMap<CoursesRootState> = {
    courses: fromCourses.reducer
}

export const getCoursesRootState = createFeatureSelector<CoursesRootState>('courses');