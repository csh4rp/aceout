
import * as fromUsers from '../reducers';
import { createSelector } from '@ngrx/store';
import { UsersState } from '../reducers/users.reducer';



export const getUserState = createSelector(fromUsers.getUsersState, (s : fromUsers.UsersRootState) => s ? s.users : undefined);
export const getUser = createSelector(getUserState, (s : UsersState) => s ? s.user : undefined);
export const getUserLoaded = createSelector(getUserState, (s: UsersState) => s ? s.loaded : undefined);
