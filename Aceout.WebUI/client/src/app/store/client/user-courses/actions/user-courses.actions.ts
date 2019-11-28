import { Action } from '@ngrx/store';
import { Pager } from 'src/app/model/pager';
import { UserCourse } from '../models/user-course.model';
import { UserCourseDetails } from '../models/user-course-details.model';


export const GET_COURSES_LIST = '[USERCOURSE] GET LIST';
export const GET_COURSES_LIST_SUCCESS = '[USERCOURSE] GET LIST SUCCESS';

export const GET_COURSE = '[USERCOURSE] GET';
export const GET_COURSE_SUCCESS = '[USERCOURSE] GET SUCCESS';

export const START_COURSE = '[USERCOURSE] START';
export const START_COURSE_SUCCESS = '[USERCORSE] START SUCCESS';

export class GetCoursesList implements Action{
    readonly type = GET_COURSES_LIST;

    constructor(public pager: Pager, public coursePathId?: number){

    }
}

export class GetCoursesListSuccess implements Action{
    readonly type = GET_COURSES_LIST_SUCCESS;

    constructor(public payload: UserCourse[]){

    }
}

export class GetCourse implements Action{
    readonly type = GET_COURSE;

    constructor(public payload: number){

    }
}

export class GetCourseSuccess implements Action{
    readonly type = GET_COURSE_SUCCESS;

    constructor(public payload: UserCourseDetails){

    }
}

export class StartCourse implements Action {
    readonly type = START_COURSE;
    constructor(public payload: number){

    }
}

export class StartCourseSuccess implements Action {
    readonly type = START_COURSE_SUCCESS;

    constructor(public payload: number){

    }
}



export type Actions = GetCoursesList | GetCoursesListSuccess | GetCourse
 | GetCourseSuccess | StartCourse | StartCourseSuccess;
