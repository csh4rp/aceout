import { Action } from "@ngrx/store";
import { CoursePath } from "../models/course-path.model";
import { CoursePathDetails } from "../models/course-path-details.model";

export const GET_COURE_PATH = '[COURSE PATH] GET';
export const GET_COURSE_PATH_SUCCESS = '[COURSE PATH] GET SUCCESS';

export const GET_COURE_PATH_LIST = '[COURSE PATH] GET LIST';
export const GET_COURSE_PATH_LIST_SUCCESS = '[COURSE PATH] GET LIST SUCCESS';

export const ADD_COURSE_PATH = '[COURSE PATH] ADD';
export const ADD_COURSE_PATH_SUCCESS = '[COURSE PATH] ADD SUCCESS';

export const UPDATE_COURSE_PATH = '[COURSE PATH] UPDATE';
export const UPDATE_COURSE_PATH_SUCCESS = '[COURSE PATH] UPDATE SUCCESS';

export const DELETE_COURSE_PATH = '[COURSE PATH] DELETE';
export const DELETE_COURSE_PATH_SUCCESS = '[COURSE PATH] DELETE SUCCESS';

export const GET_USER_LIST = '[COURSE PATH] GET USER LIST';
export const GET_USER_LIST_SUCCESS = '[COURSE PATH] GET USER LIST SUCCESS';

export class GetCoursePath implements Action{
    readonly type = GET_COURE_PATH;
    constructor(public payload: number){

    }
}

export class GetCoursePathSuccess implements Action{
    readonly type = GET_COURSE_PATH_SUCCESS;
    constructor(public payload: CoursePath){

    }
}

export class AddCoursePath implements Action{
    readonly type = ADD_COURSE_PATH;
    constructor(public payload: CoursePath){

    }
}

export class AddCoursePathSuccess implements Action{
    readonly type = ADD_COURSE_PATH_SUCCESS;
    constructor(public payload: CoursePath){

    }
}

export class UpdateCoursePath implements Action{
    readonly type = UPDATE_COURSE_PATH;
    constructor(public payload: CoursePath){

    }
}

export class UpdateCoursePathSuccess implements Action{
    readonly type = UPDATE_COURSE_PATH_SUCCESS;
    constructor(public payload: CoursePath){

    }
}


export class DeleteCoursePath implements Action{
    readonly type = DELETE_COURSE_PATH;
    constructor(public payload: number){

    }
}

export class DeleteCoursePathSuccess implements Action{
    readonly type = DELETE_COURSE_PATH_SUCCESS;
}

export class GetCoursePathList implements Action{
    readonly type = GET_COURE_PATH_LIST;
}

export class GetCoursePathListSuccess implements Action{
    readonly type = GET_COURSE_PATH_LIST_SUCCESS;
    constructor(public payload: CoursePath[]){

    }
}

export class GetUserList implements Action{
    readonly type = GET_USER_LIST;
}

export class GetUserListSuccess implements Action{
    readonly type = GET_USER_LIST_SUCCESS;
    constructor(public payload: CoursePathDetails[]){

    }
}

export type Actions = GetCoursePath | GetCoursePathSuccess | AddCoursePath | AddCoursePathSuccess | UpdateCoursePath |
UpdateCoursePathSuccess | DeleteCoursePath | DeleteCoursePathSuccess | GetCoursePathList | GetCoursePathListSuccess |
GetUserList | GetUserListSuccess;
