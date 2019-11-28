
import * as fromCoursePaths from '../reducers';
import { createSelector } from '@ngrx/store';
import { CoursePathState } from '../reducers/course-paths.reducer';

export const getCoursePathState = createSelector(fromCoursePaths.getCoursePathsRootState,
    (s : fromCoursePaths.CoursePathsRootState) => s ? s.coursePaths : undefined);
export const getCoursePath = createSelector(getCoursePathState, (s : CoursePathState) => s ? s.coursePath : undefined);
export const getCoursePaths = createSelector(getCoursePathState, (s : CoursePathState) => s ? s.paths : undefined);
export const getUserPaths = createSelector(getCoursePathState, (s : CoursePathState) => s ? s.userPaths : undefined);
export const getCoursePathsLoaded = createSelector(getCoursePathState, (s: CoursePathState) => s ? s.isLoaded : undefined);
