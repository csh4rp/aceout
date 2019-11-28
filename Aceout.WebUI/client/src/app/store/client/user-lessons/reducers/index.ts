import * as fromLessons from './user-lessons.reducer';
import { ActionReducerMap, createFeatureSelector } from '@ngrx/store';



export interface LessonsRootState {
    lessons: fromLessons.UserLessonState;
};

export const reducers : ActionReducerMap<LessonsRootState> = {
    lessons: fromLessons.reducer
}

export const getLessonsRootState = createFeatureSelector<LessonsRootState>('user-lessons');