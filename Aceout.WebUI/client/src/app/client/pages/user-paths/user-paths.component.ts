import { Store } from "@ngrx/store";
import { AppState } from "src/app/store/app.state";
import { Component, OnInit } from "@angular/core";
import { UserCourse } from "src/app/store/client/user-courses/models/user-course.model";
import { Observable } from "rxjs";
import { throwToolbarMixedModesError } from "@angular/material";
import { getCourses } from "src/app/store/client/user-courses/selectors/user-courses.selectors";
import * as UserPathActions from "src/app/store/dashboard/lms/course-paths/actions";
import { Pager } from "src/app/model/pager";
import { UserCourseDetails } from "src/app/store/client/user-courses/models/user-course-details.model";
import { Router } from "@angular/router";
import { CoursePathDetails } from "src/app/store/dashboard/lms/course-paths/models/course-path-details.model";
import { getUserPaths } from "src/app/store/dashboard/lms/course-paths/selectors/course-paths.selectors";

@Component({
    templateUrl: './user-paths.component.html',
})
export class UserPathsComponent implements OnInit {


    paths$: Observable<CoursePathDetails[]>;
    $state: Observable<any>;

    constructor(private store: Store<AppState>,
        private router: Router) {

    }

    ngOnInit() {
        this.paths$ = this.store.select(getUserPaths);

        this.store.dispatch(new UserPathActions.GetUserList());
    }

    getUrl(course: UserCourseDetails): string[] {
        if (!course) return ['/'];
        return ['courses', course.courseId.toString()];
    }
}
