import * as moment from 'moment';


export class formatHelper {

    static formatDateString(dateString: string): string {
        if (dateString) {
            let date = moment(dateString, 'DD.MM.YYYY');
            if(!date.isValid()){
                date = moment(dateString);
            }

            if(date.isValid()){
                return date.format('DD.MM.YYYY');
            }
        }
        return '';
    }

    static formatDate(date: Date): string {
        if (date) {
            var momentDate = moment.utc(date.toISOString());

            if(momentDate.isValid()){
                return momentDate.format('DD.MM.YYYY');
            }
        }
        return '';
    }

    static toISODate(dateString: string): string{
        if(!dateString) return undefined;

        let date = moment(dateString, 'DD.MM.YYYY');
        if(!date.isValid()){
            date = moment(dateString);
        }

        return date.toISOString();
    }

    static toPercentage(value: any): string{
        return (value * 100.0) + '%';
    }
}
