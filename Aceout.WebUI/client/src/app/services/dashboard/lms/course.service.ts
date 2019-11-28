import { CoursePath } from "src/app/model/dashboard/lms/course-path";
import { UrlHelper } from "src/app/app.urls";
import { HttpClient } from "@angular/common/http";
import { DataSourceProvider } from "src/app/model/dataSourceProvider";
import { Pager } from "src/app/model/pager";
import { Observable } from "rxjs";
import { DataSource } from "src/app/model/dataSource";
import { Injectable } from "@angular/core";
import { Course } from "src/app/model/dashboard/lms/courses/course";
import { CourseViewModel } from "src/app/model/dashboard/lms/courses/courseViewModel";

@Injectable({providedIn: 'root'})
export class CourseService implements DataSourceProvider<Course>{

    constructor(private urlHelper: UrlHelper, private http: HttpClient){

    }

    getData(pager: Pager, filter?: any): Observable<DataSource<Course>> {

        let queryModel = pager;
        Object.assign(queryModel, filter);

        return this.http.get<DataSource<Course>>(this.urlHelper.getUrl('lms/courses', null, queryModel));
    }

    add(record: Course) : Observable<Course>{
        return this.http.post<Course>(this.urlHelper.getUrl('lms/courses'), record);
    }

    update(record: Course) : Observable<Course>{
        return this.http.put<Course>(this.urlHelper.getUrl('lms/courses', record.id), record);
    }

    delete(id: number) : Observable<Course>{
        return this.http.delete<Course>(this.urlHelper.getUrl('lms/courses', id));
    }

    getByid(id: number) : Observable<CourseViewModel>{
        return this.http.get<CourseViewModel>(this.urlHelper.getUrl('lms/courses', id));
    }
}
