
import * as fromRoles from '../reducers';
import { createSelector } from '@ngrx/store';


export const getRolesState = createSelector(fromRoles.getRolesRootState, s =>s ? s.roles : undefined);
export const getRoles = createSelector(getRolesState, s => s ? s.roles : undefined);
export const getRolesLoaded = createSelector(getRolesState, s => s ? s.isLoaded : undefined);
export const getRolesPermissions = createSelector(getRolesState, s => s ? s.permissions : undefined);
export const getRole = createSelector(getRolesState, s => s ? s.role : undefined);