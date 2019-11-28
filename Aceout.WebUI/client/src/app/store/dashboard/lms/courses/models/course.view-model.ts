import { CourseGroup } from "./course-group.model";
import { CourseLesson } from "./course-lesson.model";

export class CourseViewModel{
    id: number;
    name: string;
    coursePathId: number;
    description: string;
    isActive: boolean;
    requireLessonOrder: boolean;
    passThreshold: number;
    groups: CourseGroup[];
    lessons: CourseLesson[];
}
