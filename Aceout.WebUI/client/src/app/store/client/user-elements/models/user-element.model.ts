import { ElementAnswer } from "./element-answer.model";
import { ElementData } from "./element-data.model";

export class UserElement {
    lessonId: number;
    materialType: number;
    elementId: number;
    position: number;
    userAnswerModels: ElementAnswer[];
    materialDataModels: ElementData[];
    correctAnswerModels: ElementAnswer[];
}
