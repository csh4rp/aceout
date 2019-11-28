import { Component, OnInit } from "@angular/core";
import { Store } from "@ngrx/store";
import { AppState } from "src/app/store/app.state";
import { UserCourse } from "src/app/store/client/user-courses/models/user-course.model";
import { Observable } from "rxjs";
import * as UserCourseSelectors from "src/app/store/client/user-courses/selectors/user-courses.selectors";
import * as UserCourseActions from "src/app/store/client/user-courses/actions/user-courses.actions";
import { UserCourseDetails } from "src/app/store/client/user-courses/models/user-course-details.model";
import { ActivatedRouteSnapshot, ActivatedRoute } from "@angular/router";
import { toLocalDateString } from "src/app/helpers/dates";



@Component({
    templateUrl: './course-details.component.html'
})
export class CourseDetailsComponent implements OnInit {


    course$: Observable<UserCourseDetails>;

    constructor(private store: Store<AppState>, private route: ActivatedRoute) {

    }

    ngOnInit() {
        this.course$ = this.store.select(UserCourseSelectors.getSelectedCourse);
        this.route.params.subscribe(p => {
            this.store.dispatch(new UserCourseActions.GetCourse(p['id']));
        })
    }

    getCourseAttempt(course: UserCourseDetails): string {

        let attempt = course.attempt ? course.attempt.toString() : '-';

        if (course.attemptLimit) {
            return attempt + '/' + course.attemptLimit.toString();
        }
        else if (!course.attemptLimit) {
            return attempt;
        }
    }

    checkCanRestart(course: UserCourseDetails): boolean {
        const attempt = course.attempt ? course.attempt : 0;
        return course.completedDate && ((course.attemptLimit && attempt < course.attempt) || !course.attempt);
    }

    startAgain(course: UserCourseDetails) {
        this.store.dispatch(new UserCourseActions.StartCourse(course.courseId));
    }



}
