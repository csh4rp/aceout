import { Component, OnInit, ChangeDetectorRef } from "@angular/core";
import { AppState } from "src/app/store/app.state";
import { Store } from "@ngrx/store";
import { ElementControl } from "../element.control";
import { UserElement } from "src/app/store/client/user-elements/models/user-element.model";
import { UserElementAnswer } from "src/app/store/client/user-elements/models/user-element-answer.model";
import { FormGroup, FormArray, FormControl } from "@angular/forms";
import { NamedFormControl } from "src/app/controls/namedFormControl";
import { ElementAnswer } from "src/app/store/client/user-elements/models/element-answer.model";

@Component({
    templateUrl: './single-answer.component.html',
    styleUrls: ['./single-answer.component.scss']
})
export class SingleAnswerComponent implements OnInit, ElementControl {

    userAnswerId: number;
    userElement: UserElement;
    form: FormGroup;
    isPreview: boolean;

    constructor(private store: Store<AppState>) {

    }

    isChecked(id: number): boolean {
        return id === this.userAnswerId;
    }

    isValid(id: number): boolean{

        if(this.userElement.correctAnswerModels.length != 1){
            return false;
        }

        const correctId = this.userElement.correctAnswerModels[0].id;
        return id === correctId;
    }

    ngOnInit() {

    }

    dataLoad(element: UserElement, isPreview: boolean) {

        this.isPreview = isPreview;
        if (element.userAnswerModels && element.userAnswerModels.length) {
            this.userAnswerId = element.userAnswerModels[0].id;
            this.form = new FormGroup({
                answer: new FormControl(this.userAnswerId)
            });
        }
        else {
            this.form = new FormGroup({
                answer: new FormControl()
            });
        }
        this.userElement = element;
    }

    getUserAnswes(): UserElementAnswer {

        const value = this.form.get('answer').value;
        const elementAnswer = new ElementAnswer();
        elementAnswer.id = parseInt(value);

        const answer = new UserElementAnswer();
        answer.elementId = this.userElement.elementId;
        answer.answers = [elementAnswer];

        return answer;
    }
}
