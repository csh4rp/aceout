import { MaterialCategory } from "src/app/model/dashboard/lms/material-category";
import { DataSourceProvider } from "src/app/model/dataSourceProvider";
import { Pager} from 'src/app/model/pager';
import {Observable } from 'rxjs';
import {DataSource} from 'src/app/model/dataSource';
import { UrlHelper } from "src/app/app.urls";
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Material } from "src/app/model/dashboard/lms/material";

@Injectable({providedIn: 'root'})
export class MaterialService implements DataSourceProvider<Material>{

    constructor(private urlHelper: UrlHelper, private http: HttpClient){

    }

    getData(pager: Pager, filter?: any): Observable<DataSource<Material>> {

        let queryModel = pager;
        Object.assign(queryModel, filter);

        return this.http.get<DataSource<Material>>(this.urlHelper.getUrl('lms/materials', null, queryModel));
    }

    add(record: Material) : Observable<Material>{
        return this.http.post<Material>(this.urlHelper.getUrl('lms/materials'), record);
    }

    update(record: Material) : Observable<Material>{
        return this.http.put<Material>(this.urlHelper.getUrl('lms/materials', record.id), record);
    }

    delete(id: number) : Observable<Material>{
        return this.http.delete<Material>(this.urlHelper.getUrl('lms/materials', id));
    }

    getByid(id: number) : Observable<Material>{
        return this.http.get<Material>(this.urlHelper.getUrl('lms/materials', id));
    }
}
