import { Action } from "@ngrx/store";


export const APP_ERROR = '[APP] ERROR';
export const APP_MESSAGE = '[APP] MESSAGE';

export class AppError implements Action {
    readonly type = APP_ERROR;

    constructor(public payload: string){

    }
}

export class AppMessage implements Action {
    readonly type = APP_MESSAGE;

    constructor(public payload: string, public redirectUrl?: string[]){

    }
}

export type Actions = AppError | AppMessage;