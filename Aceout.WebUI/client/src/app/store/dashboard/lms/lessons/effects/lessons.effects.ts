import { Injectable } from "@angular/core";
import { Effect, Actions, ofType } from '@ngrx/effects';
import * as LessonActions from '../actions';
import { switchMap, tap, map, catchError } from "rxjs/operators";
import { of } from "rxjs";
import * as CommonActions from "src/app/store/dashboard/common/actions";
import { LessonsService } from "../services/lessons.service";
import { Lesson } from "../models/lesson.model";

@Injectable()
export class LessonsEffects {

    constructor(private actions$: Actions,
        private lessonsService: LessonsService){

    }

    @Effect()
    loadLesson$ = this.actions$.pipe(ofType(LessonActions.GET_LESSON),
        switchMap((action: LessonActions.GetLesson) =>
            this.lessonsService.getById(action.payload)),
        map((data: Lesson) =>
            new LessonActions.GetLessonSuccess(data)),
        catchError(() =>
            of(new CommonActions.AppError('An error occured while loading lesson'))
        ));

    @Effect()
    addLesson$ = this.actions$.pipe(ofType(LessonActions.ADD_LESSON),
        switchMap((action: LessonActions.AddLesson) =>
            this.lessonsService.add(action.payload)),
        map((data: Lesson) =>
            new LessonActions.AddLesson(data)),
        catchError(() =>
            of(new CommonActions.AppError('An error occured while adding lesson'))
        ));

    @Effect()
    addLessonSuccess$ = this.actions$.pipe(ofType(LessonActions.ADD_LESSON_SUCCESS),
        switchMap((action: LessonActions.AddLessonSuccess) =>
            of(new CommonActions.AppMessage("Lesson created successfully", ['dashboard', 'lms', 'courses', 'edit', action.payload.courseId.toString()]))));

    @Effect()
    updateLesson$ = this.actions$.pipe(ofType(LessonActions.UPDATE_LESSON),
        switchMap((action: LessonActions.UpdateLesson) =>
            this.lessonsService.update(action.payload)),
        map((data: Lesson) =>
            new LessonActions.UpdateLessonSuccess(data)),
        catchError(() =>
            of(new CommonActions.AppError('An error occured while updating lesson'))));

    @Effect()
    updateLessonSuccess$ = this.actions$.pipe(ofType(LessonActions.UPDATE_LESSON_SUCCESS),
        switchMap((action: LessonActions.UpdateLessonSuccess) =>
            of(new CommonActions.AppMessage("Lesson updated successfully", ['dashboard', 'lms', 'courses', 'edit', action.payload.courseId.toString()]))));

    @Effect()
    deleteLesson$ = this.actions$.pipe(ofType(LessonActions.DELETE_LESSON),
        switchMap((action: LessonActions.DeleteLesson) =>
            this.lessonsService.delete(action.payload)),
        map(() =>
            new LessonActions.DeleteLessonSuccess()),
        catchError(() =>
            of(new CommonActions.AppError('An error occured while deleting lesson'))));

    @Effect()
    deleteLessonSuccess = this.actions$.pipe(ofType(LessonActions.DELETE_LESSON_SUCCESS),
        switchMap((action: LessonActions.DeleteLessonSuccess) =>
            of(new CommonActions.AppMessage("Lesson deleted successfully"))));

}
