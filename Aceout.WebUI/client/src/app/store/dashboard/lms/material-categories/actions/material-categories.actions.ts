import { Action } from "@ngrx/store";
import { MaterialCategory } from "../models/material-category.model";

export const GET_MATERIAL_CATEGORY = '[MATERIAL CATEGORY] GET';
export const GET_MATERIAL_CATEGORY_SUCCESS = '[MATERIAL CATEGORY] GET SUCCESS';

export const GET_MATERIAL_CATEGORY_LIST = '[MATERIAL CATEGORY] GET LIST';
export const GET_MATERIAL_CATEGORY_LIST_SUCCESS = '[MATERIAL CATEGORY] GET LIST SUCCESS';

export const ADD_MATERIAL_CATEGORY = '[MATERIAL CATEGORY] ADD';
export const ADD_MATERIAL_CATEGORY_SUCCESS = '[MATERIAL CATEGORY] ADD SUCCESS';

export const UPDATE_MATERIAL_CATEGORY = '[MATERIAL CATEGORY] UPDATE';
export const UPDATE_MATERIAL_CATEGORY_SUCCESS = '[MATERIAL CATEGORY] UPDATE SUCCESS';

export const DELETE_MATERIAL_CATEGORY = '[MATERIAL CATEGORY] DELETE';
export const DELETE_MATERIAL_CATEGORY_SUCCESS = '[MATERIAL CATEGORY] DELETE SUCCESS';

export class GetMaterialCategory implements Action{
    readonly type = GET_MATERIAL_CATEGORY;
    constructor(public payload: number){

    }
}

export class GetMaterialCategorySuccess implements Action{
    readonly type = GET_MATERIAL_CATEGORY_SUCCESS;
    constructor(public payload: MaterialCategory){
        
    }
}

export class GetMaterialCategoryList implements Action{
    readonly type = GET_MATERIAL_CATEGORY_LIST;
}

export class GetMaterialCategoryListSuccess implements Action{
    readonly type = GET_MATERIAL_CATEGORY_LIST_SUCCESS;
    constructor(public payload: MaterialCategory[]){
        
    }
}

export class AddMaterialCategory implements Action{
    readonly type = ADD_MATERIAL_CATEGORY;
    constructor(public payload: MaterialCategory){

    }
}

export class AddMaterialCategorySuccess implements Action{
    readonly type = ADD_MATERIAL_CATEGORY_SUCCESS;
    constructor(public payload: MaterialCategory){
        
    }
}

export class UpdateMaterialCategory implements Action{
    readonly type = UPDATE_MATERIAL_CATEGORY;
    constructor(public payload: MaterialCategory){

    }
}

export class UpdateMaterialCategorySuccess implements Action{
    readonly type = UPDATE_MATERIAL_CATEGORY_SUCCESS;
    constructor(public payload: MaterialCategory){
        
    }
}


export class DeleteMaterialCategory implements Action{
    readonly type = DELETE_MATERIAL_CATEGORY;
    constructor(public payload: number){

    }
}

export class DeleteMaterialCategorySuccess implements Action{
    readonly type = DELETE_MATERIAL_CATEGORY_SUCCESS;
}


export type Actions = GetMaterialCategory | GetMaterialCategorySuccess | AddMaterialCategory | AddMaterialCategorySuccess |
UpdateMaterialCategory | UpdateMaterialCategorySuccess | DeleteMaterialCategory | DeleteMaterialCategorySuccess |
GetMaterialCategoryList | GetMaterialCategoryListSuccess;