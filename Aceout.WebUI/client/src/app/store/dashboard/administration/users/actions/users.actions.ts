import { Action } from '@ngrx/store';
import { Pager } from 'src/app/model/pager';
import { User } from '../models/user.model';


export const GET_USER = '[USER] GET LIST';
export const GET_USER_SUCCESS = '[USER] GET LIST SUCCESS';

export const ADD_USER = '[USER] ADD';
export const ADD_USER_SUCCESS = '[USER] ADD SUCCESS';

export const UPDATE_USER = '[USER] UPDATE';
export const UPDATE_USER_SUCCESS = '[USER] UPDATE SUCCESS';

export const DELETE_USER = '[USER] DELETE';
export const DELETE_USER_SUCCESS = '[USER] DELETE SUCCESS';

export class GetUser implements Action {
    readonly type = GET_USER;
    constructor(public payload: number) {

    }
}

export class GetUsersSuccess implements Action {
    readonly type = GET_USER_SUCCESS;
    constructor(public payload: User) {

    }
}


export class AddUser implements Action {
    readonly type = ADD_USER;
    constructor(public payload: User) {

    }
}

export class AddUserSuccess implements Action {
    readonly type = ADD_USER_SUCCESS;
    constructor(public payload: User) {

    }
}

export class UpdateUser implements Action {
    readonly type = UPDATE_USER;
    constructor(public payload: User) {

    }
}

export class UpdateUserSuccess implements Action {
    readonly type = UPDATE_USER_SUCCESS;
    constructor(public payload: User) {

    }
}

export class DeleteUser implements Action {
    readonly type = DELETE_USER;
    constructor(public payload: number) {

    }
}

export class DeleteUserSuccess implements Action {
    readonly type = DELETE_USER_SUCCESS;
    constructor() {

    }
}


export type Actions = GetUser | GetUsersSuccess | AddUser | AddUserSuccess |
 UpdateUser | UpdateUserSuccess | DeleteUser | DeleteUserSuccess;
