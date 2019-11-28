import * as fromCourses from './user-courses.reducer';
import { ActionReducerMap, createFeatureSelector } from '@ngrx/store';
import { UserCourse } from '../models/user-course.model';


export interface CoursesRootState {
    courses: fromCourses.UserCoursesState;
};

export const reducers : ActionReducerMap<CoursesRootState> = {
    courses: fromCourses.reducer
}

export const getCoursesRootState = createFeatureSelector<CoursesRootState>('user-courses');