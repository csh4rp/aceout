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
    templateUrl: './menu-widget.component.html',
    selector: 'menu-widget'
})
export class MenuWidgetComponent {
constructor(){

}
}
