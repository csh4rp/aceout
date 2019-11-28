import { CoursePath } from "src/app/model/dashboard/lms/course-path";
import { UrlHelper } from "src/app/app.urls";
import { HttpClient } from "@angular/common/http";
import { DataSourceProvider } from "src/app/model/dataSourceProvider";
import { Pager } from "src/app/model/pager";
import { Observable } from "rxjs";
import { DataSource } from "src/app/model/dataSource";
import { Injectable } from "@angular/core";

@Injectable({ providedIn: 'root' })
export class CoursePathService implements DataSourceProvider<CoursePath>{

    constructor(private urlHelper: UrlHelper, private http: HttpClient) {

    }

    getData(pager: Pager, filter?: any): Observable<DataSource<CoursePath>> {

        let queryModel = pager;
        Object.assign(queryModel, filter);

        return this.http.get<DataSource<CoursePath>>(this.urlHelper.getUrl('lms/coursepaths', null, queryModel));
    }

    add(record: CoursePath): Observable<CoursePath> {
        return this.http.post<CoursePath>(this.urlHelper.getUrl('lms/coursepaths'), record);
    }

    update(record: CoursePath): Observable<CoursePath> {
        return this.http.put<CoursePath>(this.urlHelper.getUrl('lms/coursepaths', record.id), record);
    }

    delete(id: number): Observable<CoursePath> {
        return this.http.delete<CoursePath>(this.urlHelper.getUrl('lms/coursepaths', id));
    }

    getByid(id: number): Observable<CoursePath> {
        return this.http.get<CoursePath>(this.urlHelper.getUrl('lms/coursepaths', id));
    }
}
