import { CoursePath } from "src/app/model/dashboard/lms/course-path";
import { UrlHelper } from "src/app/app.urls";
import { HttpClient } from "@angular/common/http";
import { DataSourceProvider } from "src/app/model/dataSourceProvider";
import { Pager } from "src/app/model/pager";
import { Observable } from "rxjs";
import { DataSource } from "src/app/model/dataSource";
import { Injectable } from "@angular/core";
import { Course } from "../models/course.model";
import { CourseDetails } from "../models/course-details.model";
import { CourseViewModel } from "../models/course.view-model";

@Injectable({providedIn: 'root'})
export class CoursesService implements DataSourceProvider<Course>{

    constructor(private urlHelper: UrlHelper, private http: HttpClient){

    }

    getData(pager: Pager, filter?: any): Observable<DataSource<Course>> {

        let queryModel = pager;
        Object.assign(queryModel, filter);

        return this.http.get<DataSource<Course>>(this.urlHelper.getUrl('lms/courses', null, queryModel));
    }

    add(record: CourseDetails) : Observable<CourseDetails>{
        return this.http.post<CourseDetails>(this.urlHelper.getUrl('lms/courses'), record);
    }

    update(record: CourseDetails) : Observable<CourseDetails>{
        return this.http.put<CourseDetails>(this.urlHelper.getUrl('lms/courses', record.id), record);
    }

    delete(id: number) : Observable<any>{
        return this.http.delete<any>(this.urlHelper.getUrl('lms/courses', id));
    }

    getById(id: number) : Observable<CourseViewModel>{
        return this.http.get<CourseViewModel>(this.urlHelper.getUrl('lms/courses', id));
    }
}
