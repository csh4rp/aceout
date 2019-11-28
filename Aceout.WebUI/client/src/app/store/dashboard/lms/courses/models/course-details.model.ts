import { CourseGroup } from "./course-group.model";

export class CourseDetails {
    id: number;
    name: string;
    coursePathId: number;
    description: string;
    isActive: boolean;
    requireLessonOrder: boolean;
    passThreshold: number;
    groups: CourseGroup[];
    lessonIds: number[];
}
