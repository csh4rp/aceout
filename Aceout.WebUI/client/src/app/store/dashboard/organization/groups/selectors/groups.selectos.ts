
import * as fromGroups from '../reducers';
import { createSelector } from '@ngrx/store';
import { GroupsState } from '../reducers/groups.reducer';



export const getGroupsState = createSelector(fromGroups.getGroupsRootState, (s : fromGroups.GroupsRootState) => s ? s.groups : undefined);
export const getGroup = createSelector(getGroupsState, (s : GroupsState) => s ? s.group : undefined);
export const getGroups = createSelector(getGroupsState, (s : GroupsState) => s ? s.groups : undefined);
export const getGroupsLoaded = createSelector(getGroupsState, (s: GroupsState) => s ? s.loaded : undefined);
