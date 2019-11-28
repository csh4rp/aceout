import { LessonElement } from "./lesson-element";

export class Lesson{
    id: number;
    name: string;
    type: number;
    description: string;
    courseId: number;
    attemptCount: number;
    passResult: number;
    isActive: boolean;
    allowAnswerCheck: boolean;
    allowAnswerPreview: boolean;
    elements: LessonElement[];
}