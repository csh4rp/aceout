import { Action } from '@ngrx/store';
import { Pager } from 'src/app/model/pager';
import { RoleDetails } from '../models/role-details.model';
import { Permission } from '../models/permission';


export const GET_ROLES_LIST = '[ROLE] GET LIST';
export const GET_ROLES_LIST_SUCCESS = '[ROLE] GET LIST SUCCESS';

export const GET_ROLE = '[ROLE] GET';
export const GET_ROLE_SUCCESS = '[ROLE]'

export const ADD_ROLE = '[ROLE] ADD';
export const ADD_ROLE_SUCCESS = '[ROLE] ADD SUCCESS';

export const UPDATE_ROLE = '[ROLE] UPDATE';
export const UPDATE_ROLE_SUCCESS = '[ROLE] UPDATE SUCCESS';

export const DELETE_ROLE = '[ROLE] DELETE';
export const DELETE_ROLE_SUCCESS = '[ROLE] DELETE SUCCESS';

export const GET_PERMISSIONS = '[ROLE] GET PERMISSIONS';
export const GET_PERMISSIONS_SUCCESS = '[ROLE] GET PERMISSIONS SUCCESS';

export class GetRolesList implements Action{
    readonly type = GET_ROLES_LIST;
    constructor(public payload: Pager){

    }
}

export class GetRolesListSuccess implements Action{
    readonly type = GET_ROLES_LIST_SUCCESS;
    constructor(public payload: RoleDetails[]){

    }
}

export class GetRole implements Action {
    readonly type = GET_ROLE;
    constructor(public payload: number){

    }
}

export class GetRoleSuccess implements Action {
    readonly type = GET_ROLE_SUCCESS;
    constructor(public payload: RoleDetails){

    }
}

export class AddRole implements Action {
    readonly type = ADD_ROLE;
    constructor(public payload: RoleDetails){

    }
}

export class AddRoleSuccess implements Action {
    readonly type = ADD_ROLE_SUCCESS;
    constructor(public payload: RoleDetails){

    }
}

export class UpdateRole implements Action {
    readonly type = UPDATE_ROLE;
    constructor(public payload: RoleDetails){

    }
}

export class UpdateRoleSuccess implements Action {
    readonly type = UPDATE_ROLE_SUCCESS;
    constructor(public payload: RoleDetails){

    }
}

export class DeleteRole implements Action {
    readonly type = DELETE_ROLE;
    constructor(public payload: number){

    }
}

export class DeleteRoleSuccess implements Action {
    readonly type = DELETE_ROLE_SUCCESS;

}

export class GetPermissions implements Action{
    readonly type = GET_PERMISSIONS;
}

export class GetPermissionsSuccess implements Action{
    readonly type = GET_PERMISSIONS_SUCCESS;
    constructor(public payload: Permission[]){

    }
}

export type Actions = GetRolesList | GetRolesListSuccess | GetRole | GetRoleSuccess |
AddRole | AddRoleSuccess | UpdateRole | UpdateRoleSuccess | DeleteRole | DeleteRoleSuccess |
GetPermissions | GetPermissionsSuccess;
