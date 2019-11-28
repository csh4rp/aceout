import * as RolesActions from '../actions';
import { RoleDetails } from '../models/role-details.model';
import { Permission } from '../models/permission';

export interface RolesState {
    roles: RoleDetails[];
    permissions: Permission[],
    role: RoleDetails,
    isLoaded: boolean;
}


const initialState: RolesState = {
    roles: [],
    permissions: [],
    role: null,
    isLoaded: false,
};

export function reducer(state: RolesState = initialState, action: RolesActions.Actions) {
    switch (action.type) {
        case RolesActions.GET_ROLES_LIST:
            return {
                ...state,
                isLoaded: false
            };

        case RolesActions.GET_ROLES_LIST_SUCCESS:
            return {
                ...state,
                roles: action.payload,
                isLoaded: true
            };

        case RolesActions.GET_ROLE:
            return {
                ...state,
                isLoaded: false
            };

        case RolesActions.GET_ROLE_SUCCESS:
            return {
                ...state,
                role: action.payload,
                isLoaded: true
            };

        case RolesActions.GET_PERMISSIONS:
            return {
                ...state,
                permissions: null,
                isLoaded: false
            };

        case RolesActions.GET_PERMISSIONS_SUCCESS:
            return {
                ...state,
                isLoaded: true,
                permissions: action.payload
            };

        case RolesActions.DELETE_ROLE:
            return {
                ...state,
                isLoaded: false
            };

        case RolesActions.DELETE_ROLE_SUCCESS:
           return {
               ...state,
               isLoaded: true
           };

        default:
            return {
                ...state
            };
    }
}
