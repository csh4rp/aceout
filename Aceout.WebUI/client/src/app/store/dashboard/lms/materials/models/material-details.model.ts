import { MaterialData } from "./material-data.model";
import { AnswerData } from "./answer-data.model";

export class MaterialDetails {
    id: number;
    name: string;
    content: string;
    categoryId: number;
    type: number;
    answerModels: AnswerData[];
    dataModels: MaterialData[];
}