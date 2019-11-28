import { UserLesson } from "./user-lesson.model";
import { UserCourse } from "./user-course.model";

export class UserCourseDetails{
    id: number;
    courseId: number;
    name: string;
    isStarted: boolean;
    startedDate:string;
    completedDate: string;
    attempt: number;
    attemptLimit: number;
    result: number;
    lessons: UserLesson[];
    previousAttempts: UserCourse[];
}
