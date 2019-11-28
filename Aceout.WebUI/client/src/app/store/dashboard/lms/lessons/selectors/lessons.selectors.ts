
import * as fromLessons from '../reducers';
import { createSelector } from '@ngrx/store';
import { LessonsState } from '../reducers/lessons.reducer';



export const getMaterialCategoriesState = createSelector(fromLessons.getLessonsRootState, 
    (s : fromLessons.LessonsRootState) => s ? s.lessons : undefined);
export const getLesson = createSelector(getMaterialCategoriesState, (s : LessonsState) => s ? s.lesson : undefined);
export const getLessonsLoaded = createSelector(getMaterialCategoriesState, (s: LessonsState) => s ? s.isLoaded : undefined);
