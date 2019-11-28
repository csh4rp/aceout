import { TranslateService } from '@ngx-translate/core';

export class GridControl {
    protected translate: TranslateService;
    constructor(translate: TranslateService){
        this.translate = translate;
    }

    public onGridReady(params: any, columns: any[]) {
        window.addEventListener("resize", function () {
            setTimeout(function () {
                params.api.sizeColumnsToFit();
            });
        });

        const ids = columns.reduce((acc, t) => ({...acc, [t.headerName]: t.colId}), {});
        const keys = columns.map(x => x.headerName);

        this.translate.get(keys).subscribe(str => {
            for (let prop in str) {
                const value = str[prop];
                const column = params.columnApi.getColumn(ids[prop]);
                let colDef = column.getColDef();
                if(colDef.headerName != ''){
                    colDef.headerName = value;
                }
            }
            params.api.refreshHeader();
        });
    }
}
