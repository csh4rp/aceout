import { Injectable } from "@angular/core";
import { Effect, Actions, ofType } from '@ngrx/effects';
import * as UserLessonsActions from '../actions';
import { switchMap, tap, map } from "rxjs/operators";
import { of } from "rxjs";
import { Store } from "@ngrx/store";
import { UserLessonsService } from "../services/user-lessons.service";
import { UserLessonDetails } from "../models/user-lesson-details.model";
import { Router, ActivatedRoute } from "@angular/router";

@Injectable()
export class UserLessonsEffects {


    constructor(private actions$: Actions,
        private route: ActivatedRoute,
        private lessonsService: UserLessonsService,
        private router: Router) {

    }

    @Effect()
    loadLesson$ = this.actions$.pipe(ofType(UserLessonsActions.GET_LESSON),
        switchMap((action: UserLessonsActions.GetLesson) =>
            this.lessonsService.getById(action.payload)),
        map((data: UserLessonDetails) => new UserLessonsActions.GetLessonSuccess(data)
        ));


    @Effect({ dispatch: false })
    loadLessonSuccess$ = this.actions$.pipe(ofType(UserLessonsActions.GET_LESSON_SUCCESS),
        tap((action: UserLessonsActions.GetLessonSuccess) => {
            if (action.payload.startedDate && !action.payload.completedDate && action.payload.isAccessible) {
                this.router.navigate(['.', action.payload.lessonId, '0'], { relativeTo: this.route });
            }

        }));

    @Effect()
    startLesson$ = this.actions$.pipe(ofType(UserLessonsActions.START_LESSON),
        switchMap((action: UserLessonsActions.StartLesson) =>
            this.lessonsService.startAttempt(action.payload)),
        map((data: any) => new UserLessonsActions.StartLessonSuccess(data.lessonId)));

    @Effect({ dispatch: false })
    startLessonSuccess$ = this.actions$.pipe(ofType(UserLessonsActions.START_LESSON_SUCCESS),
        tap((action: UserLessonsActions.StartLessonSuccess) => {
            this.router.navigate(['.', action.payload.lessonId, '0'], { relativeTo: this.route })
        }));


    @Effect()
    finishLesson$ = this.actions$.pipe(ofType(UserLessonsActions.FINISH_LESSON),
        switchMap((action: UserLessonsActions.FinishLesson) =>
            this.lessonsService.finishAttempt(action.payload)),
        map((data: any) => new UserLessonsActions.FinishLessonSuccess(data)));

    @Effect({ dispatch: false })
    finishLessonSuccess$ = this.actions$.pipe(ofType(UserLessonsActions.FINISH_LESSON_SUCCESS),
        tap((action: UserLessonsActions.FinishLessonSuccess) => {
            this.router.navigate(['../', action.payload.lessonId])
        }));
}
