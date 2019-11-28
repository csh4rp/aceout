import * as moment from 'moment';
import { formatHelper } from 'src/app/helpers/formatHelper';

export class CourseGroup{
    id: number;
    name: string;
    fromDate: string;
    toDate: string;
    attemptCount: number;

    get fromDateLocal() : string{
        const utc = moment.utc(this.fromDate);
        return formatHelper.formatDate(utc.toDate());
    }
}
