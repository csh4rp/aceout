import { UserElement } from "src/app/store/client/user-elements/models/user-element.model";
import { ElementAnswer } from "src/app/store/client/user-elements/models/element-answer.model";
import { UserElementAnswer } from "src/app/store/client/user-elements/models/user-element-answer.model";

export interface ElementControl {
    dataLoad(element: UserElement, isPreview: boolean);
    getUserAnswes(): UserElementAnswer;
}
