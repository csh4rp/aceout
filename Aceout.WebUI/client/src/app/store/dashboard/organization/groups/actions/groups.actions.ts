import { Action } from "@ngrx/store";
import { Group } from "../models/group.model";
import { GroupDetails } from "../models/group.details.model";

export const GET_GROUP = '[GROUP] GET';
export const GET_GROUP_SUCCESS = '[GROUP] GET SUCCESS';

export const GET_GROUP_LIST = '[GROUP] GET LIST';
export const GET_GROUP_LIST_SUCCESS = '[GROUP] GET LIST SUCCESS';

export const ADD_GROUP = '[GROUP] ADD';
export const ADD_GROUP_SUCCESS = '[GROUP] ADD SUCCESS';

export const UPDATE_GROUP = '[GROUP] UPDATE';
export const UPDATE_GROUP_SUCCESS = '[GROUP] UPDATE SUCCESS';

export const DELETE_GROUP = '[GROUP] DELETE';
export const DELETE_GROUP_SUCCESS = '[GROUP] DELETE SUCCESS';

export class GetGroup implements Action {
    readonly type = GET_GROUP;
    constructor(public payload: number){

    }
}

export class GetGroupSuccess implements Action {
    readonly type = GET_GROUP_SUCCESS;
    constructor(public payload: GroupDetails){
        
    }
}

export class AddGroup implements Action {
    readonly type = ADD_GROUP;
    constructor(public payload: Group){
        
    }
}

export class AddGroupSuccess implements Action {
    readonly type = ADD_GROUP_SUCCESS;
    constructor(public payload: Group){
        
    }
}

export class UpdateGroup implements Action {
    readonly type = UPDATE_GROUP;
    constructor(public payload: Group){
        
    }
}


export class UpdateGroupSuccess implements Action {
    readonly type = UPDATE_GROUP_SUCCESS;
    constructor(public payload: Group){
        
    }
}

export class DeleteGroup implements Action {
    readonly type = DELETE_GROUP;
    constructor(public payload: number){
        
    }
}

export class DeleteGroupSuccess implements Action {
    readonly type = DELETE_GROUP_SUCCESS;
}

export class GetGroupList implements Action {
    readonly type = GET_GROUP_LIST;
}

export class GetGroupListSuccess implements Action {
    readonly type = GET_GROUP_LIST_SUCCESS;
    constructor(public payload: Group[]){
        
    }
}

export type Actions = GetGroup | GetGroupSuccess | AddGroup | AddGroupSuccess | UpdateGroup | UpdateGroupSuccess |
DeleteGroup | DeleteGroupSuccess | GetGroupList | GetGroupListSuccess;