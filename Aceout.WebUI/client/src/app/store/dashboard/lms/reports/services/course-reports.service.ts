import { Injectable } from "@angular/core";
import { CourseReport } from "../models/course-report.model";
import { HttpClient } from "@angular/common/http";
import { UrlHelper } from "src/app/app.urls";
import { Pager } from "src/app/model/pager";
import { Observable } from "rxjs";
import { DataSource } from "src/app/model/dataSource";
import { DataSourceProvider } from "src/app/model/dataSourceProvider";

@Injectable({providedIn: 'root'})
export class CourseReportsService implements DataSourceProvider<CourseReport>{

    constructor(private urlHelper: UrlHelper, private http: HttpClient){

    }

    getData(pager: Pager, filter?: any): Observable<DataSource<CourseReport>> {

        let queryModel = pager;
        Object.assign(queryModel, filter);

        return this.http.get<DataSource<CourseReport>>(this.urlHelper.getUrl('lms/course-reports', null, queryModel));
    }
}
