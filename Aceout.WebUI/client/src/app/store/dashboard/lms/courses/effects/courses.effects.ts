import { Injectable } from "@angular/core";
import { Effect, Actions, ofType } from '@ngrx/effects';
import * as CourseActions from '../actions';
import { switchMap, tap, map, catchError } from "rxjs/operators";
import { of } from "rxjs";
import { Store } from "@ngrx/store";
import { MatSnackBar } from "@angular/material";
import { SnackBarComponent } from "src/app/shared-dashboard/snack-bar/snack-bar.component";
import { Location } from "@angular/common";
import * as CommonActions from "src/app/store/dashboard/common/actions";
import { CoursesService } from "../services/courses.service";
import { CourseDetails } from "../models/course-details.model";
import { CourseViewModel } from "../models/course.view-model";


@Injectable()
export class CoursesEffects {

    constructor(private actions$: Actions,
        private coursesService: CoursesService){
     
    }

    @Effect()
    loadCourse$ = this.actions$.pipe(ofType(CourseActions.GET_COURSE),
        switchMap((action: CourseActions.GetCourse) =>
            this.coursesService.getById(action.payload)),
        map((data: CourseViewModel) =>
            new CourseActions.GetCourseSuccess(data)),
        catchError(() =>
            of(new CommonActions.AppError('An error occured while loading course'))
        ));

    @Effect()
    addCourse$ = this.actions$.pipe(ofType(CourseActions.ADD_COURSE),
        switchMap((action: CourseActions.AddCourse) =>
            this.coursesService.add(action.payload)),
        map((data: CourseDetails) =>
            new CourseActions.AddCourseSuccess(data)),
        catchError(() =>
            of(new CommonActions.AppError('An error occured while adding course'))
        ));

    @Effect()
    addCourseSuccess$ = this.actions$.pipe(ofType(CourseActions.ADD_COURSE_SUCCESS),
        switchMap((action: CourseActions.AddCourseSuccess) =>
            of(new CommonActions.AppMessage("Course created successfully", ['dashboard', 'lms', 'courses']))));

    @Effect()
    updateCourse$ = this.actions$.pipe(ofType(CourseActions.UPDATE_COURSE),
        switchMap((action: CourseActions.UpdateCourse) =>
            this.coursesService.update(action.payload)),
        map((data: CourseDetails) =>
            new CourseActions.UpdateCourseSuccess(data)),
        catchError(() =>
            of(new CommonActions.AppError('An error occured while updating course'))));

    @Effect()
    updateCourseSuccess$ = this.actions$.pipe(ofType(CourseActions.UPDATE_COURSE_SUCCESS),
        switchMap((action: CourseActions.UpdateCourseSuccess) =>
            of(new CommonActions.AppMessage("Course updated successfully", ['dashboard', 'lms', 'courses']))));

    @Effect()
    deleteCourse$ = this.actions$.pipe(ofType(CourseActions.DELETE_COURSE),
        switchMap((action: CourseActions.DeleteCourse) =>
            this.coursesService.delete(action.payload)),
        map(() =>
            new CourseActions.DeleteCourseSuccess()),
        catchError(() =>
            of(new CommonActions.AppError('An error occured while deleting course'))));

    @Effect()
    deleteCourseSuccess = this.actions$.pipe(ofType(CourseActions.DELETE_COURSE_SUCCESS),
        switchMap((action: CourseActions.DeleteCourseSuccess) =>
            of(new CommonActions.AppMessage("Course deleted successfully"))));

}