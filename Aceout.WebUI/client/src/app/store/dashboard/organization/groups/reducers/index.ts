import * as fromGroups from './groups.reducer';
import { ActionReducerMap, createFeatureSelector } from '@ngrx/store';



export interface GroupsRootState {
    groups: fromGroups.GroupsState;
};

export const reducers : ActionReducerMap<GroupsRootState> = {
    groups: fromGroups.reducer
}

export const getGroupsRootState = createFeatureSelector<GroupsRootState>('groups');