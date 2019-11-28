import { Pager } from './pager';
import { Observable } from 'rxjs';
import { DataSource } from './dataSource';

export interface DataSourceProvider<TModel>{
    getData(pager: Pager, filter?: any) : Observable<DataSource<TModel>>;
}