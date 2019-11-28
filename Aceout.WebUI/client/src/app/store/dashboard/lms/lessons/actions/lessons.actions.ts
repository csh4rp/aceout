import { Action } from "@ngrx/store";
import { Lesson } from "../models/lesson.model";

export const GET_LESSON = '[LESSON] GET';
export const GET_LESSON_SUCCESS = '[LESSON] GET SUCCESS';
export const ADD_LESSON = '[LESSON] ADD';
export const ADD_LESSON_SUCCESS = '[LESSON] ADD SUCCESS';
export const UPDATE_LESSON = '[LESSON] UPDATE';
export const UPDATE_LESSON_SUCCESS = '[LESSON] UPDATE SUCCESS';
export const DELETE_LESSON = '[LESSON] DELETE';
export const DELETE_LESSON_SUCCESS = '[LESSON] DELETE SUCCESS';


export class GetLesson implements Action {
    readonly type = GET_LESSON;
    constructor(public payload: number){

    }
}

export class GetLessonSuccess implements Action {
    readonly type = GET_LESSON_SUCCESS;
    constructor(public payload: Lesson){
        
    }
}

export class AddLesson implements Action {
    readonly type = ADD_LESSON;
    constructor(public payload: Lesson){
        
    }
}

export class AddLessonSuccess implements Action {
    readonly type = ADD_LESSON_SUCCESS;
    constructor(public payload: Lesson){
        
    }
}

export class UpdateLesson implements Action {
    readonly type = UPDATE_LESSON;
    constructor(public payload: Lesson){
        
    }
}

export class UpdateLessonSuccess implements Action {
    readonly type = UPDATE_LESSON_SUCCESS;
    constructor(public payload: Lesson){
        
    }
}

export class DeleteLesson implements Action {
    readonly type = DELETE_LESSON;
    constructor(public payload: number){
        
    }
}

export class DeleteLessonSuccess implements Action {
    readonly type = DELETE_LESSON_SUCCESS;
}

export type Actions = GetLesson | GetLessonSuccess | AddLesson | AddLessonSuccess | UpdateLesson |
UpdateLessonSuccess | DeleteLesson | DeleteLessonSuccess;