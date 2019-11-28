import { Injectable } from "@angular/core";
import { Effect, Actions, ofType } from '@ngrx/effects';
import * as UserCoursesActions from '../actions';
import { switchMap, tap, map } from "rxjs/operators";
import { UserCoursesService } from "../services/user-courses.service";
import { of } from "rxjs";
import { Store } from "@ngrx/store";
import { UserCourse } from "../models/user-course.model";
import { DataSource } from "src/app/model/dataSource";
import { UserCourseDetails } from "../models/user-course-details.model";
import { Router } from "@angular/router";

@Injectable()
export class UserCoursesEffects {

    constructor(private actions$: Actions,
        private coursesService: UserCoursesService,
        private router: Router) {

    }

    @Effect()
    loadCourses$ = this.actions$.pipe(ofType(UserCoursesActions.GET_COURSES_LIST),
        switchMap((action: UserCoursesActions.GetCoursesList) =>
            this.coursesService.getCourses(action.pager, action.coursePathId)),
        map((data: DataSource<UserCourse>) => new UserCoursesActions.GetCoursesListSuccess(data.data)
        ));

    @Effect()
    loadCourse$ = this.actions$.pipe(ofType(UserCoursesActions.GET_COURSE),
        switchMap((action: UserCoursesActions.GetCourse) =>
            this.coursesService.getById(action.payload)),
        map((data: UserCourseDetails) => new UserCoursesActions.GetCourseSuccess(data)
        ));

    @Effect()
    startCourse$ = this.actions$.pipe(ofType(UserCoursesActions.START_COURSE),
        switchMap((action: UserCoursesActions.StartCourse) =>
            this.coursesService.startCourse(action.payload)),
        map((data: any) => new UserCoursesActions.StartCourseSuccess(data.courseId)));

    @Effect({ dispatch: false })
    startCourseSuccess$ = this.actions$.pipe(ofType(UserCoursesActions.START_COURSE_SUCCESS),
        tap((action: UserCoursesActions.GetCourseSuccess) => {
            this.router.navigate(['courses', action.payload]);
        }));
}
