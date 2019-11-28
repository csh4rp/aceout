import { Injectable } from "@angular/core";
import { Effect, Actions, ofType } from '@ngrx/effects';
import * as CoursePathActions from '../actions';
import { switchMap, tap, map, catchError } from "rxjs/operators";
import { of } from "rxjs";
import { Store } from "@ngrx/store";
import { MatSnackBar } from "@angular/material";
import { SnackBarComponent } from "src/app/shared-dashboard/snack-bar/snack-bar.component";
import { Location } from "@angular/common";
import * as CommonActions from "src/app/store/dashboard/common/actions";
import { CoursePathsService } from "../services/course-paths.service";
import { CoursePath } from "../models/course-path.model";
import { CoursePathDetails } from "../models/course-path-details.model";

@Injectable()
export class CoursePathsEffects {

    constructor(private actions$: Actions,
        private coursePathsService: CoursePathsService) {

    }

    @Effect()
    loadCoursePath$ = this.actions$.pipe(ofType(CoursePathActions.GET_COURE_PATH),
        switchMap((action: CoursePathActions.GetCoursePath) =>
            this.coursePathsService.getById(action.payload)),
        map((data: CoursePath) =>
            new CoursePathActions.GetCoursePathSuccess(data)),
        catchError(() =>
            of(new CommonActions.AppError('An error occured while loading course path'))
        ));

    @Effect()
    loadUserPaths$ = this.actions$.pipe(ofType(CoursePathActions.GET_USER_LIST),
        switchMap((action: CoursePathActions.GetUserList) =>
            this.coursePathsService.getUserList()),
        map((data: CoursePathDetails[]) =>
            new CoursePathActions.GetUserListSuccess(data)));

    @Effect()
    loadCoursePathList$ = this.actions$.pipe(ofType(CoursePathActions.GET_COURE_PATH_LIST),
        switchMap((action: CoursePathActions.GetCoursePathList) =>
            this.coursePathsService.getList()),
        map((data: CoursePath[]) =>
            new CoursePathActions.GetCoursePathListSuccess(data)),
        catchError(() =>
            of(new CommonActions.AppError('An error occured while loading course paths'))
        ));

    @Effect()
    addCoursePath$ = this.actions$.pipe(ofType(CoursePathActions.ADD_COURSE_PATH),
        switchMap((action: CoursePathActions.AddCoursePath) =>
            this.coursePathsService.add(action.payload)),
        map((data: CoursePath) =>
            new CoursePathActions.AddCoursePathSuccess(data)),
        catchError(() =>
            of(new CommonActions.AppError('An error occured while adding course path'))
        ));

    @Effect()
    addCoursePathSuccess$ = this.actions$.pipe(ofType(CoursePathActions.ADD_COURSE_PATH_SUCCESS),
        switchMap((action: CoursePathActions.AddCoursePathSuccess) =>
            of(new CommonActions.AppMessage("Course path created successfully", ['dashboard', 'lms', 'course-paths']))));

    @Effect()
    updateCoursePath$ = this.actions$.pipe(ofType(CoursePathActions.UPDATE_COURSE_PATH),
        switchMap((action: CoursePathActions.UpdateCoursePath) =>
            this.coursePathsService.update(action.payload)),
        map((data: CoursePath) =>
            new CoursePathActions.UpdateCoursePathSuccess(data)),
        catchError(() =>
            of(new CommonActions.AppError('An error occured while updating course path'))));

    @Effect()
    updateCoursePathSuccess$ = this.actions$.pipe(ofType(CoursePathActions.UPDATE_COURSE_PATH_SUCCESS),
        switchMap((action: CoursePathActions.UpdateCoursePathSuccess) =>
            of(new CommonActions.AppMessage("Course path updated successfully", ['dashboard', 'lms', 'course-paths']))));

    @Effect()
    deleteCoursePath$ = this.actions$.pipe(ofType(CoursePathActions.DELETE_COURSE_PATH),
        switchMap((action: CoursePathActions.DeleteCoursePath) =>
            this.coursePathsService.delete(action.payload)),
        map(() =>
            new CoursePathActions.DeleteCoursePathSuccess()),
        catchError(() =>
            of(new CommonActions.AppError('An error occured while deleting course path'))));

    @Effect()
    deleteCoursePathSuccess = this.actions$.pipe(ofType(CoursePathActions.DELETE_COURSE_PATH_SUCCESS),
        switchMap((action: CoursePathActions.DeleteCoursePathSuccess) =>
            of(new CommonActions.AppMessage("Course path deleted successfully"))));

}
