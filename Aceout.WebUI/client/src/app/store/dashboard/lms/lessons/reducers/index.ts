import * as fromLessons from './lessons.reducer';
import { ActionReducerMap, createFeatureSelector } from '@ngrx/store';

export interface LessonsRootState {
    lessons: fromLessons.LessonsState;
};

export const reducers : ActionReducerMap<LessonsRootState> = {
    lessons: fromLessons.reducer
}

export const getLessonsRootState = createFeatureSelector<LessonsRootState>('lessons');