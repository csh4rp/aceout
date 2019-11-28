import { Injectable } from "@angular/core";
import { Pager } from "src/app/model/pager";
import { Observable } from "rxjs";
import { DataSource } from "src/app/model/dataSource";
import { UserCourse } from "../models/user-course.model";
import { HttpClient } from "@angular/common/http";
import { UrlHelper } from "src/app/app.urls";
import { UserCourseDetails } from "../models/user-course-details.model";

@Injectable({ providedIn: 'root' })
export class UserCoursesService {

    constructor(private http: HttpClient, private urlHelper: UrlHelper) {

    }

    getCourses(pager: Pager, coursePathId?: number): Observable<DataSource<UserCourse>> {

        let queryModel = pager;
        if(coursePathId){
            Object.assign(queryModel, {coursePathId: coursePathId});
        }

        return this.http.get<DataSource<UserCourse>>(this.urlHelper.getUrl('lms/user-courses', null, queryModel));;
    }

    getById(id: number): Observable<UserCourseDetails> {
        return this.http.get<UserCourseDetails>(this.urlHelper.getUrl('lms/user-courses', id));
    }

    startCourse(id: number): Observable<any> {
        return this.http.post<any>(this.urlHelper.getUrl('lms/user-courses/start-attempt'), { courseId: id });
    }
}
