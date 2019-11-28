import * as fromUsers from './users.reducer';
import { ActionReducerMap, createFeatureSelector } from '@ngrx/store';
import { User } from '../models/user.model';


export interface UsersRootState {
    users: fromUsers.UsersState;
};

export const reducers : ActionReducerMap<UsersRootState> = {
    users: fromUsers.reducer
}

export const getUsersState = createFeatureSelector<UsersRootState>('users');