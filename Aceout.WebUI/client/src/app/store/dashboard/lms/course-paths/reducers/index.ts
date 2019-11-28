import * as fromCoursePaths from './course-paths.reducer';
import { ActionReducerMap, createFeatureSelector } from '@ngrx/store';
import { CoursePath } from '../models/course-path.model';


export interface CoursePathsRootState {
    coursePaths: fromCoursePaths.CoursePathState;
};

export const reducers : ActionReducerMap<CoursePathsRootState> = {
    coursePaths: fromCoursePaths.reducer
}

export const getCoursePathsRootState = createFeatureSelector<CoursePathsRootState>('coursePaths');