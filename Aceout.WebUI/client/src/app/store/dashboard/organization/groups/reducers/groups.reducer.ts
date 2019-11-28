import { Group } from "../models/group.model";
import * as GroupActions from "../actions/";
import { GroupDetails } from "../models/group.details.model";

export interface GroupsState {
    group: GroupDetails,
    groups: Group[],
    loaded: boolean
};

const initialState: GroupsState = {
    group: null,
    groups: [],
    loaded: false
};

export function reducer(state: GroupsState = initialState, action: GroupActions.Actions) {
    switch (action.type) {

        case GroupActions.ADD_GROUP:
            return {
                ...state,
                loaded: false
            };

        case GroupActions.ADD_GROUP_SUCCESS:
            return {
                ...state,
                loaded: true
            };

        case GroupActions.GET_GROUP:
            return {
                ...state,
                loaded: false
            };

        case GroupActions.GET_GROUP_SUCCESS:
            return {
                ...state,
                group: action.payload,
                loaded: true
            };

        case GroupActions.UPDATE_GROUP:
            return {
                ...state,
                loaded: false
            };
        case GroupActions.UPDATE_GROUP_SUCCESS:
            return {
                ...state,
                loaded: true
            };

        case GroupActions.DELETE_GROUP:
            return {
                ...state,
                loaded: false
            };

        case GroupActions.DELETE_GROUP_SUCCESS:
            return {
                ...state,
                loaded: true
            };

        case GroupActions.GET_GROUP_LIST:
            return{
                ...state,
                loaded: false,
            };

        case GroupActions.GET_GROUP_LIST_SUCCESS:
            return{
                ...state,
                groups: action.payload,
                loaded: true
            };

        default:
            return {
                ...state
            };
    }
}

