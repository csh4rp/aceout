import { Store } from "@ngrx/store";
import { AppState } from "src/app/store/app.state";
import { Component, OnInit } from "@angular/core";
import { UserCourse } from "src/app/store/client/user-courses/models/user-course.model";
import { Observable } from "rxjs";
import { getCourses } from "src/app/store/client/user-courses/selectors/user-courses.selectors";
import * as InfoActions from "src/app/store/dashboard/cms/informations/actions"
import { Pager } from "src/app/model/pager";
import { UserCourseDetails } from "src/app/store/client/user-courses/models/user-course-details.model";
import { Router } from "@angular/router";
import { Information } from "src/app/store/dashboard/cms/informations/models/infotmation.model";
import { getInformationsList } from "src/app/store/dashboard/cms/informations/selectors/informations.selectors";

@Component({
    templateUrl: './user-informations.component.html',
})
export class UserInformationsComponent implements OnInit {

    infos$: Observable<Information[]>;

    constructor(private store: Store<AppState>) {

    }

    ngOnInit() {
        this.infos$ = this.store.select(getInformationsList);
        this.store.dispatch(new InfoActions.GetInformationList(0, 30));
    }
}
