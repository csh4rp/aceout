import { Group } from "../../organization/group";
import { Lesson } from "../lessons/lesson";

export class CourseViewModel{
    id: number;
    name: string;
    coursePathId: number;
    description: string;
    fromDate: Date;
    toDate: Date;
    groups: Group[];
    lessons: Lesson[];
}