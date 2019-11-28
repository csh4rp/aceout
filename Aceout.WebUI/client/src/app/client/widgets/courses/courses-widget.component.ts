import { Store } from "@ngrx/store";
import { AppState } from "src/app/store/app.state";
import { Component, OnInit } from "@angular/core";
import { UserCourse } from "src/app/store/client/user-courses/models/user-course.model";
import { Observable } from "rxjs";
import { throwToolbarMixedModesError } from "@angular/material";
import { getCourses } from "src/app/store/client/user-courses/selectors/user-courses.selectors";
import * as UserCourseActions from "src/app/store/client/user-courses/actions/user-courses.actions";
import { Pager } from "src/app/model/pager";
import { UserCourseDetails } from "src/app/store/client/user-courses/models/user-course-details.model";
import { Router } from "@angular/router";

@Component({
    templateUrl: './courses-widget.component.html',
    selector: 'user-courses-widget'
})
export class CoursesWidgetComponent implements OnInit {


    courses$: Observable<UserCourse[]>;
    $state: Observable<any>;

    constructor(private store: Store<AppState>,
        private router: Router) {

    }

    ngOnInit() {
        const pager = new Pager(4, 0, 'id');
        this.courses$ = this.store.select(getCourses);

        this.store.dispatch(new UserCourseActions.GetCoursesList(pager));
    }

    navigateToCourse(course: UserCourseDetails) {
        if (course.isStarted) {
            this.router.navigate(['courses', course.courseId]);
        }
        else {
            this.store.dispatch(new UserCourseActions.StartCourse(course.courseId));
        }
    }

    getUrl(course: UserCourseDetails): string[] {
        if (!course) return ['/'];
        return ['courses', course.courseId.toString()];
    }
}
