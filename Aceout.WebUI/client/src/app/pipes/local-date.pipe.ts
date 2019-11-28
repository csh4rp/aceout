import { PipeTransform, Pipe } from "@angular/core";
import { toLocalDateString } from "../helpers/dates";

@Pipe({ name: 'localdate' })
export class LocalDatePipe implements PipeTransform {
    transform(value: any, ...args: any[]) {
        if (!value) return '-';
        return toLocalDateString(value);
    }

}
