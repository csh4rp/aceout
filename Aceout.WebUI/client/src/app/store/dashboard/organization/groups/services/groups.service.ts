import { DataSourceProvider } from "src/app/model/dataSourceProvider";
import { Pager } from 'src/app/model/pager';
import { Observable } from 'rxjs';
import { DataSource } from 'src/app/model/dataSource';
import { UrlHelper } from "src/app/app.urls";
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Group } from "../models/group.model";
import { GroupDetails } from "../models/group.details.model";

@Injectable({ providedIn: 'root' })
export class GroupsService implements DataSourceProvider<Group>{

    constructor(private urlHelper: UrlHelper, private http: HttpClient) {

    }

    getData(pager: Pager, filter?: any): Observable<DataSource<Group>> {

        const queryModel = pager;
        Object.assign(queryModel, filter);

        return this.http.get<DataSource<Group>>(this.urlHelper.getUrl('organization/groups', null, queryModel));
    }

    add(record: Group): Observable<Group> {
        return this.http.post<Group>(this.urlHelper.getUrl('organization/groups'), record);
    }

    update(record: Group): Observable<Group> {
        return this.http.put<Group>(this.urlHelper.getUrl('organization/groups', record.id), record);
    }

    delete(id: number): Observable<any> {
        return this.http.delete<any>(this.urlHelper.getUrl('organization/groups', id));
    }

    getByid(id: number): Observable<GroupDetails> {
        return this.http.get<GroupDetails>(this.urlHelper.getUrl('organization/groups', id));
    }

    getList(): Observable<Group[]> {
        return this.http.get<Group[]>(this.urlHelper.getUrl('organization/groups/list'));
    }

    autocomplete(searchQuery: string): Observable<Group[]> {
        const queryModel = {
            searchQuery: searchQuery
        };

        return this.http.get<Group[]>(this.urlHelper.getUrl('organization/groups/autocomplete', undefined, queryModel));
    }
}
