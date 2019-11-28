import { Action } from "@ngrx/store";
import { CourseDetails } from "../models/course-details.model";
import { CourseViewModel } from "../models/course.view-model";

export const GET_COURSE = '[COURSE] GET';
export const GET_COURSE_SUCCESS = '[COURSE] GET SUCCESS';

export const ADD_COURSE = '[COURSE] ADD';
export const ADD_COURSE_SUCCESS = '[COURSE] ADD SUCCESS';

export const UPDATE_COURSE = '[COURSE] UPDATE';
export const UPDATE_COURSE_SUCCESS = '[COURSE] UPDATE SUCCESS';

export const DELETE_COURSE = '[COURSE] DELETE';
export const DELETE_COURSE_SUCCESS = '[COURSE] DELETE SUCCESS';

export class GetCourse implements Action {
    readonly type = GET_COURSE;
    constructor(public payload: number){
        
    }
}

export class GetCourseSuccess implements Action {
    readonly type = GET_COURSE_SUCCESS;
    constructor(public payload: CourseViewModel){
        
    }
}

export class AddCourse implements Action {
    readonly type = ADD_COURSE;
    constructor(public payload: CourseDetails){
        
    }
}

export class AddCourseSuccess implements Action {
    readonly type = ADD_COURSE_SUCCESS;
    constructor(public payload: CourseDetails){
        
    }
}

export class UpdateCourse implements Action {
    readonly type = UPDATE_COURSE;
    constructor(public payload: CourseDetails){
        
    }
}

export class UpdateCourseSuccess implements Action {
    readonly type = UPDATE_COURSE_SUCCESS;
    constructor(public payload: CourseDetails){
        
    }
}

export class DeleteCourse implements Action {
    readonly type = DELETE_COURSE;
    constructor(public payload: number){
        
    }
}

export class DeleteCourseSuccess implements Action {
    readonly type = DELETE_COURSE_SUCCESS;
}

export type Actions = GetCourse | GetCourseSuccess | AddCourse | AddCourseSuccess | UpdateCourse | UpdateCourseSuccess |
DeleteCourse | DeleteCourseSuccess;