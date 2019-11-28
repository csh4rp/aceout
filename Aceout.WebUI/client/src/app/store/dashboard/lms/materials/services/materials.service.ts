import { DataSourceProvider } from "src/app/model/dataSourceProvider";
import { Pager} from 'src/app/model/pager';
import {Observable } from 'rxjs';
import {DataSource} from 'src/app/model/dataSource';
import { UrlHelper } from "src/app/app.urls";
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Material } from "../models/material.model";
import { MaterialDetails } from "../models/material-details.model";


@Injectable({providedIn: 'root'})
export class MaterialsService implements DataSourceProvider<Material>{

    constructor(private urlHelper: UrlHelper, private http: HttpClient){

    }

    getData(pager: Pager, filter?: any): Observable<DataSource<Material>> {

        let queryModel = pager;
        Object.assign(queryModel, filter);

        return this.http.get<DataSource<Material>>(this.urlHelper.getUrl('lms/materials', null, queryModel));
    }

    add(record: MaterialDetails) : Observable<MaterialDetails>{
        console.log('sent')
        return this.http.post<MaterialDetails>(this.urlHelper.getUrl('lms/materials'), record);
    }

    update(record: MaterialDetails) : Observable<MaterialDetails>{
        return this.http.put<MaterialDetails>(this.urlHelper.getUrl('lms/materials', record.id), record);
    }

    delete(id: number) : Observable<Material>{
        return this.http.delete<Material>(this.urlHelper.getUrl('lms/materials', id));
    }

    getById(id: number) : Observable<MaterialDetails>{
        return this.http.get<MaterialDetails>(this.urlHelper.getUrl('lms/materials', id));
    }
}
