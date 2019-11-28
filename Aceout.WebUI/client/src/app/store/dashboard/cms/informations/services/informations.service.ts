import { UrlHelper } from "src/app/app.urls";
import { HttpClient } from "@angular/common/http";
import { DataSourceProvider } from "src/app/model/dataSourceProvider";
import { Pager } from "src/app/model/pager";
import { Observable } from "rxjs";
import { DataSource } from "src/app/model/dataSource";
import { Injectable } from "@angular/core";
import { Information } from "../models/infotmation.model";
import { InformationDetails } from "../models/information-details.model";
import { InformationViewModel } from "../models/information.view-model";

@Injectable({providedIn: 'root'})
export class InformationsService implements DataSourceProvider<Information>{

    constructor(private urlHelper: UrlHelper, private http: HttpClient){

    }

    getData(pager: Pager, filter?: any): Observable<DataSource<Information>> {
        let queryModel = pager;
        Object.assign(queryModel, filter);

        return this.http.get<DataSource<Information>>(this.urlHelper.getUrl('cms/informations', null, queryModel));
    }

    getList(pageNumber: number, count: number): Observable<Information[]> {
        return this.http.get<Information[]>(this.urlHelper.getUrl('cms/informations/list/' + pageNumber + '/' + count,));
    }

    add(record: InformationDetails) : Observable<InformationDetails>{
        return this.http.post<InformationDetails>(this.urlHelper.getUrl('cms/informations'), record);
    }

    update(record: InformationDetails) : Observable<InformationDetails>{
        return this.http.put<InformationDetails>(this.urlHelper.getUrl('cms/informations', record.id), record);
    }

    delete(id: number) : Observable<any>{
        return this.http.delete<any>(this.urlHelper.getUrl('cms/informations', id));
    }

    getById(id: number) : Observable<InformationViewModel>{
        return this.http.get<InformationViewModel>(this.urlHelper.getUrl('cms/informations', id));
    }
}
