import * as fromLessons from '../reducers';
import { createSelector } from '@ngrx/store';


export const getUserLessonState = createSelector(fromLessons.getLessonsRootState, s => s.lessons);
export const getLesson = createSelector(getUserLessonState, s => s.lesson);
export const getIsLessonStarted = createSelector(getUserLessonState, s => s.isStarted);
export const getIsLessonCompleted = createSelector(getUserLessonState, s => s.isCompleted);

