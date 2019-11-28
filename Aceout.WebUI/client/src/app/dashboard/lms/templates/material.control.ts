import { MaterialData } from "src/app/model/dashboard/lms/material-data";
import { AnswerData } from "src/app/model/dashboard/lms/answer-data";
import { Observable } from "rxjs";

export interface MaterialControl{
    getDataModels() : MaterialData[];
    getAnswerModels() : AnswerData[];

    setModels(dataModels: MaterialData[], answerModels: AnswerData[]);

    isValid: boolean;
}
