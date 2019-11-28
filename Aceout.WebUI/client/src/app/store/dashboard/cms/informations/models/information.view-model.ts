import { GroupInformation } from "./group-information.model";

export class InformationViewModel{
    id: number;
    fromDate: string;
    toDate: string;
    author: string;
    title: string;
    content: string;
    groups: GroupInformation[];
}
