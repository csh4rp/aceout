import { MaterialCategory } from "src/app/model/dashboard/lms/material-category";
import { DataSourceProvider } from "src/app/model/dataSourceProvider";
import { Pager } from 'src/app/model/pager';
import { Observable } from 'rxjs';
import { DataSource } from 'src/app/model/dataSource';
import { UrlHelper } from "src/app/app.urls";
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable({ providedIn: 'root' })
export class MaterialCategoriesService implements DataSourceProvider<MaterialCategory>{

    constructor(private urlHelper: UrlHelper, private http: HttpClient) {

    }

    getData(pager: Pager, filter?: any): Observable<DataSource<MaterialCategory>> {

        let queryModel = pager;
        Object.assign(queryModel, filter);

        return this.http.get<DataSource<MaterialCategory>>(this.urlHelper.getUrl('lms/materialcategories', null, queryModel));
    }

    add(record: MaterialCategory): Observable<MaterialCategory> {
        console.log(1);
        return this.http.post<MaterialCategory>(this.urlHelper.getUrl('lms/materialcategories'), record);
    }

    update(record: MaterialCategory): Observable<MaterialCategory> {
        return this.http.put<MaterialCategory>(this.urlHelper.getUrl('lms/materialcategories', record.id), record);
    }

    delete(id: number): Observable<MaterialCategory> {
        return this.http.delete<MaterialCategory>(this.urlHelper.getUrl('lms/materialcategories', id));
    }

    getByid(id: number): Observable<MaterialCategory> {
        return this.http.get<MaterialCategory>(this.urlHelper.getUrl('lms/materialcategories', id));
    }

    getList(): Observable<MaterialCategory[]> {
        return this.http.get<MaterialCategory[]>(this.urlHelper.getUrl('lms/materialcategories/list'));
    }
}
