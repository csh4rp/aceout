import * as UsersActions from '../actions';
import { User } from '../models/user.model';

export interface UsersState{
    user: User;
    loaded: boolean,
}


const initialState: UsersState = {
    user: null,
    loaded: false
};

export function reducer(state: UsersState = initialState, action: UsersActions.Actions){
    switch(action.type){
        
        case UsersActions.GET_USER:
            return {
                user: state.user,
                loaded: false
            };

        case UsersActions.GET_USER_SUCCESS:
            return {
                user: action.payload,
                loaded: true
            };

        case UsersActions.ADD_USER:
            return {
                ...state,
                loaded: false
            };
        
        case UsersActions.ADD_USER_SUCCESS:
            return {
                user: action.payload,
                loaded: true
            };


        case UsersActions.UPDATE_USER:
            return {
                ...state,
                loaded: false
            };

        case UsersActions.UPDATE_USER_SUCCESS:
            return {
                user: action.payload,
                loaded: true
            };

        case UsersActions.DELETE_USER:
            return {
                ...state,
                loaded: false
            };
        
        case UsersActions.DELETE_USER_SUCCESS:
            return {
                ...state,
                loaded: true
            };
    }
}
