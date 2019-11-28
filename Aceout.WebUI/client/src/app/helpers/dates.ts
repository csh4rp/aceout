import * as moment from 'moment';

export const DATE_FORMAT = 'DD.MM.YYYY';

export function toLocalDateString(dateString: string): string{
    console.log('local:' + dateString);
    if(isNotISODateFormat(dateString)){
        console.log('invoked');
        return dateString;
    }
    const utc = moment.utc(dateString);
    return utc.local().format(DATE_FORMAT);
}

export function toISODateString(dateString: string): string{
    console.log(dateString)
    if(isInISODateFormat(dateString)){
        return dateString;
    }

    return moment(dateString, DATE_FORMAT).toISOString();
}

export function isNotISODateFormat(date: string): boolean{
    const regex = /^(-?(?:[1-9][0-9]*)?[0-9]{4})-(1[0-2]|0[1-9])-(3[01]|0[1-9]|[12][0-9])T(2[0-3]|[01][0-9]):([0-5][0-9]):([0-5][0-9])(.[0-9]+)??$/;
    
    return (!regex.test(date) && moment(date, DATE_FORMAT).isValid())
}

export function isInISODateFormat(date: string): boolean{
    const regex = /^(-?(?:[1-9][0-9]*)?[0-9]{4})-(1[0-2]|0[1-9])-(3[01]|0[1-9]|[12][0-9])T(2[0-3]|[01][0-9]):([0-5][0-9]):([0-5][0-9])(.[0-9]+)??$/;
    return regex.test(date);
}