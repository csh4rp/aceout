import { Action } from "@ngrx/store";
import { UserElement } from "../models/user-element.model";
import { UserElementAnswer } from "../models/user-element-answer.model";

export const GET_ELEMENT_LIST = '[USERELEMENT] GET LIST';
export const GET_ELEMENT_LIST_SUCCESS = '[USERELEMENT] GET LIST SUCCESS';

export const SAVE_ELEMENT = '[USERELEMENT] SAVE';
export const SAVE_ELEMENT_SUCCESS= '[USERELEMENT] SAVE SUCCESS';

export const NAVIGATE_NEXT = '[USERELEMENT] NEXT';
export const NAVIGATE_NEXT_SUCCESS = '[USERELEMENT] NEXT SUCCESS';
export const NAVIGATE_PREVIOUS = '[USERELEMENT] PREVIOUS';
export const NAVIGATE_PREVIOUS_SUCCESS = '[USERELEMENT] PREVIOUS SUCCESS';

export class GetElements implements Action {
    readonly type = GET_ELEMENT_LIST;
    constructor(public payload: number){
    }
}

export class GetElementsSuccess implements Action {
    readonly type = GET_ELEMENT_LIST_SUCCESS;
    constructor(public payload: UserElement[]){

    }
}

export class SaveElement implements Action {
    readonly type = SAVE_ELEMENT;
    constructor(public payload: UserElementAnswer){

    }
}

export class SaveElementSuccess implements Action {
    readonly type = SAVE_ELEMENT_SUCCESS;
    constructor(public payload: UserElementAnswer){

    }
}

export class NavigateNext implements Action {
    readonly type = NAVIGATE_NEXT;
    constructor(public position: number){

    }
}

export class NavigateNextSuccess implements Action {
    readonly type = NAVIGATE_NEXT_SUCCESS;

}


export class NavigatePrevious implements Action {
    readonly type = NAVIGATE_PREVIOUS;
    constructor(public position: number){
        
    }
}


export type Actions = GetElements | GetElementsSuccess | SaveElement | SaveElementSuccess |
NavigateNext | NavigatePrevious;