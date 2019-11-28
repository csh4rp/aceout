import { GridDataSource } from 'src/app/model/gridDataSource';
import { HttpClient } from '@angular/common/http';
import { DataSourceProvider } from 'src/app/model/dataSourceProvider';
import {Pager} from 'src/app/model/pager';
import {DataSource} from 'src/app/model/dataSource';
import {Observable} from 'rxjs';
import { UrlHelper } from 'src/app/app.urls';
import { Injectable } from '@angular/core';
import { map, catchError, share } from 'rxjs/operators';

@Injectable({providedIn: 'root'})
export class RoleService implements DataSourceProvider<RoleModel> {

    errorHandle(obj: any) : Observable<boolean>{
        console.log(obj)
        return Observable.create(o => o.next(true));
    }

    getData(pager:  Pager, filter?: any): Observable<DataSource<RoleModel>> {
        let query = Object.assign(pager, filter);
        return this.http.get<DataSource<RoleModel>>(this.urlHelper.getUrl("dashboard/roles", null, query));
    }

    add(record: RoleModel) : Observable<RoleModel>{
        return this.http.post<RoleModel>(this.urlHelper.getUrl('dashboard/roles'), record).pipe(share());
    }

    update(record: RoleModel) : Observable<boolean>{
        return  this.http.put<RoleModel>(this.urlHelper.getUrl('dashboard/roles', record.id), record).pipe(map(d => true), share());
    }

    delete(id: number) : Observable<RoleModel>{
        return this.http.delete<RoleModel>(this.urlHelper.getUrl('dashboard/roles', id)).pipe(share());
    }

    getById(id: number) : Observable<RoleModel>{
        return this.http.get<RoleModel>(this.urlHelper.getUrl('dashboard/roles', id));
    }

    constructor(private http: HttpClient, private urlHelper: UrlHelper){

    }



}

export class RoleModel{
    id: number;
    name: string;
    permissions: string[];
}
