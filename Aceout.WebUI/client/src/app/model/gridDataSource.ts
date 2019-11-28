import { Pager } from './pager';
import { Observable } from 'rxjs';
import { DataSource } from './dataSource';
import { DataSourceProvider } from './dataSourceProvider';
import { SortModel } from './sortModel';

export class GridDataSource {
    constructor(private dataSource: DataSourceProvider<any>, private defaultSort: SortModel, private filter? : any) {

    }

    getRows(params: any) {
        const pageSize = params.endRow - params.startRow;
        const pageNumber = params.startRow / pageSize;

        let sortBy = '';

        if (params.sortModel.length > 0) {
            const sortModel = params.sortModel[0];

            if (sortModel.sort === 'desc') {
                sortBy += '-';
            }

            sortBy += sortModel.colId;
        }
        else {
            sortBy = this.defaultSort.getExpression();
        }

        const pager = new Pager(pageSize, pageNumber, sortBy);

        this.dataSource.getData(pager, this.filter).subscribe(res => {
            params.successCallback(res.data, res.rowCount);
        });
    }
}