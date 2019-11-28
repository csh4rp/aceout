import { Action } from "@ngrx/store";
import { Information } from "../models/infotmation.model";
import { InformationDetails } from "../models/information-details.model";
import { InformationViewModel } from "../models/information.view-model";

export const GET_INFORMATION = '[INFORMATION] GET';
export const GET_INFORMATION_SUCCESS = '[INFORMATION] GET SUCCESS';

export const GET_INFORMATION_LIST = '[INFORMATION] GET LIST';
export const GET_INFORMATION_LIST_SUCCESS = '[INFORMATION] GET LIST SUCCESS';

export const ADD_INFORMATION = '[INFORMATION] ADD';
export const ADD_INFORMATION_SUCCESS = '[INFORMATION] ADD SUCCESS';

export const UPDATE_INFORMATION = '[INFORMATION] UPDATE';
export const UPDATE_INFORMATION_SUCCESS = '[INFORMATION] UPDATE SUCCESS';

export const DELETE_INFORMATION = '[INFORMATION] DELETE';
export const DELETE_INFORMATION_SUCCESS = '[INFORMATION] DELETE SUCCESS';

export class GetInformation implements Action{
    readonly type = GET_INFORMATION;
    constructor(public payload: number){

    }
}

export class GetInformationSuccess implements Action{
    readonly type = GET_INFORMATION_SUCCESS;
    constructor(public payload: InformationViewModel){

    }
}

export class AddInformation implements Action{
    readonly type = ADD_INFORMATION;
    constructor(public payload: InformationDetails){

    }
}

export class AddInformationSuccess implements Action{
    readonly type = ADD_INFORMATION_SUCCESS;
    constructor(public payload: InformationDetails){

    }
}

export class UpdateInformation implements Action{
    readonly type = UPDATE_INFORMATION;
    constructor(public payload: InformationDetails){

    }
}

export class UpdateInformationSuccess implements Action{
    readonly type = UPDATE_INFORMATION_SUCCESS;
    constructor(public payload: InformationDetails){

    }
}


export class DeleteInformation implements Action{
    readonly type = DELETE_INFORMATION;
    constructor(public payload: number){

    }
}

export class DeleteInformationSuccess implements Action{
    readonly type = DELETE_INFORMATION_SUCCESS;
}

export class GetInformationList implements Action{
    readonly type = GET_INFORMATION_LIST;
    constructor(public pageNumber: number, public count: number){

    }
}

export class GetInformationListSuccess implements Action{
    readonly type = GET_INFORMATION_LIST_SUCCESS;
    constructor(public payload: Information[]){

    }
}

export type Actions = GetInformation | GetInformationSuccess | AddInformation | AddInformationSuccess | UpdateInformation |
UpdateInformationSuccess | DeleteInformation | DeleteInformationSuccess | GetInformationList | GetInformationListSuccess;
