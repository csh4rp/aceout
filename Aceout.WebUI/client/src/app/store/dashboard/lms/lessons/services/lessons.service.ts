import { CoursePath } from "src/app/model/dashboard/lms/course-path";
import { UrlHelper } from "src/app/app.urls";
import { HttpClient } from "@angular/common/http";
import { DataSourceProvider } from "src/app/model/dataSourceProvider";
import { Pager } from "src/app/model/pager";
import { Observable } from "rxjs";
import { DataSource } from "src/app/model/dataSource";
import { Injectable } from "@angular/core";
import { Lesson } from "../models/lesson.model";

@Injectable({providedIn: 'root'})
export class LessonsService implements DataSourceProvider<Lesson>{

    constructor(private urlHelper: UrlHelper, private http: HttpClient){

    }

    getData(pager: Pager, filter?: any): Observable<DataSource<Lesson>> {

        let queryModel = pager;
        Object.assign(queryModel, filter);

        return this.http.get<DataSource<Lesson>>(this.urlHelper.getUrl('lms/lessons', null, queryModel));
    }

    add(record: Lesson) : Observable<Lesson>{
        return this.http.post<Lesson>(this.urlHelper.getUrl('lms/lessons'), record);
    }

    update(record: Lesson) : Observable<Lesson>{
        return this.http.put<Lesson>(this.urlHelper.getUrl('lms/lessons', record.id), record);
    }

    delete(id: number) : Observable<Lesson>{
        return this.http.delete<Lesson>(this.urlHelper.getUrl('lms/lessons', id));
    }

    getById(id: number) : Observable<Lesson>{
        return this.http.get<Lesson>(this.urlHelper.getUrl('lms/lessons', id));
    }
}
