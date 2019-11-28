import { UserLesson } from "./user-lesson.model";

export class UserLessonDetails {
    userLessonId: number;
    lessonId: number;
    startedDate: string;
    completedDate: string;
    isAccessible: boolean;
    attempt: number;
    attemptLimit: number;
    name: string;
    type: number;
    isPassed: boolean;
    elementCount: number;
    allowAnswerCheck:boolean;
    previousAttempts: UserLesson[];
}
