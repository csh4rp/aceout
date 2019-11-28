import { Action } from "@ngrx/store";
import { MaterialDetails } from "../models/material-details.model";

export const GET_MATERIAL = '[MATERIAL] GET';
export const GET_MATERIAL_SUCCESS = '[MATERIAL] GET SUCCESS';
export const ADD_MATERIAL = '[MATERIAL] ADD';
export const ADD_MATERIAL_SUCCESS = '[MATERIAL] ADD SUCCESS';
export const UPDATE_MATERIAL = '[MATERIAL] UPDATE';
export const UPDATE_MATERIAL_SUCCESS = '[MATERIAL] UPDATE SUCCESS';
export const DELETE_MATERIAL = '[MATERIAL] DELETE';
export const DELETE_MATERIAL_SUCCESS = '[MATERIAL] DELETE SUCCESS';


export class GetMaterial implements Action {
    readonly type = GET_MATERIAL;
    constructor(public payload: number){

    }
}

export class GetMaterialSuccess implements Action {
    readonly type = GET_MATERIAL_SUCCESS;
    constructor(public payload: MaterialDetails){
        
    }
}

export class AddMaterial implements Action {
    readonly type = ADD_MATERIAL;
    constructor(public payload: MaterialDetails){
        console.log('invoked');
    }
}

export class AddMaterialSuccess implements Action {
    readonly type = ADD_MATERIAL_SUCCESS;
    constructor(public payload: MaterialDetails){
        
    }
}

export class UpdateMaterial implements Action {
    readonly type = UPDATE_MATERIAL;
    constructor(public payload: MaterialDetails){
        
    }
}

export class UpdateMaterialSuccess implements Action {
    readonly type = UPDATE_MATERIAL_SUCCESS;
    constructor(public payload: MaterialDetails){
        
    }
}

export class DeleteMaterial implements Action {
    readonly type = DELETE_MATERIAL;
    constructor(public payload: number){
        
    }
}

export class DeleteMaterialSuccess implements Action {
    readonly type = DELETE_MATERIAL_SUCCESS;
}

export type Actions = GetMaterial | GetMaterialSuccess | AddMaterial | AddMaterialSuccess | UpdateMaterial |
UpdateMaterialSuccess | DeleteMaterial | DeleteMaterialSuccess;