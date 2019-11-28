import { UrlHelper } from 'src/app/app.urls';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { extend } from 'webdriver-js-extender';
import { catchError, map, share, refCount, publishLast, publishReplay } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { DataSource } from 'src/app/model/dataSource';
import { Pager } from 'src/app/model/pager';
import { DataSourceProvider } from 'src/app/model/dataSourceProvider';
import { User } from '../models/user.model';

@Injectable({providedIn: 'root'})
export class UserService implements DataSourceProvider<User>{



    constructor(private urlHelper: UrlHelper, private http: HttpClient){

    }

    errorHandle(obj: any) : Observable<boolean>{
        return Observable.create(o => o.next(true));
    }

    getData(pager: Pager, filter? : any): Observable<DataSource<User>> {

        let queryModel = pager;
        Object.assign(queryModel, filter);

        return this.http.get<DataSource<User>>(this.urlHelper.getUrl('dashboard/users/', null, queryModel));
    }

    add(record: User) : Observable<User>{
        return this.http.post<User>(this.urlHelper.getUrl('dashboard/users'), record);
    }

    update(record: User) : Observable<any>{
        return  this.http.put<User>(this.urlHelper.getUrl('dashboard/users', record.id), record);
    }

    delete(id: number) : Observable<any>{
        return this.http.delete<any>(this.urlHelper.getUrl('dashboard/users', id));
    }

    getById(id: number) : Observable<User>{
        return this.http.get<User>(this.urlHelper.getUrl('dashboard/users', id));
    }

    checkUsername(userName: string, id?: number) : Observable<any>{
        let queryModel = {};

        if(id){
            queryModel = {
                id: id,
                userName: userName
            };
        }
        else{
            queryModel = {
                userName: userName
            }
        }

        return this.http.get(this.urlHelper.getUrl('dashboard/users/checkusername', null, queryModel));
    }

    autocomplete(searchQuery: string) : Observable<User[]>{

        const queryModel = {
            searchQuery: searchQuery
        };

        return this.http.get<User[]>(this.urlHelper.getUrl('dashboard/users/autocomplete', null, queryModel));
    }
}

