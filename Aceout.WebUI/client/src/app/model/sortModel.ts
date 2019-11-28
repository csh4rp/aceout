export class SortModel{
    constructor(public property: string, public direction: string){

    }

    public getExpression(){

        let expression = '';

        if(this.direction == 'desc'){
            return '-' + this.property;
        }

        return this.property;
    }
}