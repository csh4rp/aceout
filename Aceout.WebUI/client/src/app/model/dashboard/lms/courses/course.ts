export class Course{
    id: number;
    name: string;
    coursePathId: number;
    description: string;
    fromDate: Date;
    toDate: Date;
    groupIds: number[];
    lessonIds: number[];
}