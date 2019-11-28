import { MaterialData } from "./material-data";
import { AnswerData } from "./answer-data";

export class Material{
    id: number;
    name: string;
    content: string;
    categoryId: number;
    type: number;
    answers: AnswerData[];
    datas: MaterialData[];
}