import { Action } from '@ngrx/store';
import { Pager } from 'src/app/model/pager';
import { UserLessonDetails } from '../models/user-lesson-details.model';

export const GET_LESSON = '[USERLESSON] GET';
export const GET_LESSON_SUCCESS = '[USERLESSON] GET SUCCESS';

export const START_LESSON = '[USERLESSON] START';
export const START_LESSON_SUCCESS = '[USERLESSON] START SUCCESS';

export const FINISH_LESSON = '[USERLESSON] FINISH';
export const FINISH_LESSON_SUCCESS = '[USERLESSON] FINISH SUCCESS';

export const PREVIEW_LESSON = '[USERLESSON] PREVIEW';
export const PREVIEW_LESSON_SUCCESS = '[USERLESSON] PREVIEW SUCCESS';

export const CHECK_LESSON = '[USERLESSON] CHECK';
export const CHECK_LESSON_SUCCESS = '[USERLESSON] CHECK SUCCESS';

export class GetLesson implements Action {
    readonly type = GET_LESSON;

    constructor(public payload: number) {
    }
}

export class GetLessonSuccess implements Action {
    readonly type = GET_LESSON_SUCCESS;

    constructor(public payload: UserLessonDetails) {

    }
}


export class StartLesson implements Action {
    readonly type = START_LESSON;

    constructor(public payload: number) {

    }
}

export class StartLessonSuccess implements Action {
    readonly type = START_LESSON_SUCCESS;
    constructor(public payload: any){

    }

}

export class FinishLesson implements Action {
    readonly type = FINISH_LESSON;

    constructor(public payload: number) {

    }
}

export class FinishLessonSuccess implements Action {
    readonly type = FINISH_LESSON_SUCCESS;
    constructor(public payload: any){

    }
}

export class CheckLesson implements Action {
    readonly type = CHECK_LESSON;

    constructor(public payload: number) {

    }
}

export class CheckLessonSuccess implements Action {
    readonly type = CHECK_LESSON_SUCCESS;
    constructor(public payload: any){

    }

}


export class PreviewLesson implements Action {
    readonly type = PREVIEW_LESSON;

    constructor(public payload: number) {

    }
}

export class PreviewLessonSuccess implements Action {
    readonly type = PREVIEW_LESSON_SUCCESS;
    constructor(public payload: any){

    }

}

export type Actions = GetLesson | GetLessonSuccess | StartLesson | StartLessonSuccess |
 FinishLesson | FinishLessonSuccess | CheckLesson | CheckLessonSuccess | PreviewLesson | PreviewLessonSuccess;
