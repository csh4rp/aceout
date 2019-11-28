import * as fromRoles from './roles.reducer';
import { ActionReducerMap, createFeatureSelector } from '@ngrx/store';
import { RoleDetails } from '../models/role-details.model';


export interface RolesState {
    roles: fromRoles.RolesState;
};

export const reducers : ActionReducerMap<RolesState> = {
    roles: fromRoles.reducer
}

export const getRolesRootState = createFeatureSelector<RolesState>('roles');