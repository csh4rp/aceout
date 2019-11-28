import { GridDataSource } from 'src/app/model/gridDataSource';
import { HttpClient } from '@angular/common/http';
import { DataSourceProvider } from 'src/app/model/dataSourceProvider';
import {Pager} from 'src/app/model/pager';
import {DataSource} from 'src/app/model/dataSource';
import {Observable} from 'rxjs';
import { UrlHelper } from 'src/app/app.urls';
import { Injectable } from '@angular/core';
import { map, catchError, share } from 'rxjs/operators';
import { RoleDetails } from '../models/role-details.model';
import { Permission } from '../models/permission';

@Injectable({providedIn: 'root'})
export class RoleService implements DataSourceProvider<RoleDetails> {

    errorHandle(obj: any) : Observable<boolean>{
        console.log(obj)
        return Observable.create(o => o.next(true));
    }

    getData(pager:  Pager, filter?: any): Observable<DataSource<RoleDetails>> {
        let query = Object.assign(pager, filter);
        return this.http.get<DataSource<RoleDetails>>(this.urlHelper.getUrl("dashboard/roles", null, query));
    }

    add(record: RoleDetails) : Observable<RoleDetails>{
        return this.http.post<RoleDetails>(this.urlHelper.getUrl('dashboard/roles'), record).pipe(share());
    }

    update(record: RoleDetails) : Observable<RoleDetails>{
        return  this.http.put<RoleDetails>(this.urlHelper.getUrl('dashboard/roles', record.id), record);
    }

    delete(id: number) : Observable<any>{
        return this.http.delete<any>(this.urlHelper.getUrl('dashboard/roles', id));
    }

    getById(id: number) : Observable<RoleDetails>{
        return this.http.get<RoleDetails>(this.urlHelper.getUrl('dashboard/roles', id));
    }

    getPermissions() : Observable<Permission[]>{
        return this.http.get<Permission[]>(this.urlHelper.getUrl('dashboard/roles/permissions/', null, null));

    }

    constructor(private http: HttpClient, private urlHelper: UrlHelper){

    }



}
