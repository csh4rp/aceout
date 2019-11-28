import { UrlHelper } from "src/app/app.urls";
import { HttpClient } from "@angular/common/http";
import { DataSourceProvider } from "src/app/model/dataSourceProvider";
import { Pager } from "src/app/model/pager";
import { Observable } from "rxjs";
import { DataSource } from "src/app/model/dataSource";
import { Injectable } from "@angular/core";
import { CoursePath } from "../models/course-path.model";

@Injectable({providedIn: 'root'})
export class CoursePathsService implements DataSourceProvider<CoursePath>{

    constructor(private urlHelper: UrlHelper, private http: HttpClient){

    }

    getData(pager: Pager, filter?: any): Observable<DataSource<CoursePath>> {

        let queryModel = pager;
        Object.assign(queryModel, filter);

        return this.http.get<DataSource<CoursePath>>(this.urlHelper.getUrl('lms/course-paths', null, queryModel));
    }

    getList(): Observable<CoursePath[]> {
        return this.http.get<CoursePath[]>(this.urlHelper.getUrl('lms/course-paths/list'));
    }

    getUserList(): Observable<CoursePath[]> {
        return this.http.get<CoursePath[]>(this.urlHelper.getUrl('lms/course-paths/user-list'));
    }

    add(record: CoursePath) : Observable<CoursePath>{
        return this.http.post<CoursePath>(this.urlHelper.getUrl('lms/course-paths'), record);
    }

    update(record: CoursePath) : Observable<CoursePath>{
        return this.http.put<CoursePath>(this.urlHelper.getUrl('lms/course-paths', record.id), record);
    }

    delete(id: number) : Observable<any>{
        return this.http.delete<any>(this.urlHelper.getUrl('lms/course-paths', id));
    }

    getById(id: number) : Observable<CoursePath>{
        return this.http.get<CoursePath>(this.urlHelper.getUrl('lms/course-paths', id));
    }
}
