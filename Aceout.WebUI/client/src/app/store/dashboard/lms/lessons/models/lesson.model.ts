import { LessonElement } from "./lesson-element.model";

export class Lesson {
    id: number;
    name: string;
    type: number;
    description: string;
    courseId: number;
    attemptCount: number;
    passThreshold: number;
    isActive: boolean;
    allowAnswerCheck: boolean;
    allowAnswerPreview: boolean;
    elements: LessonElement[];
}
