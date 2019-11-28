
import * as InformationActions from "../actions";
import { Information } from "../models/infotmation.model";
import { InformationViewModel } from "../models/information.view-model";

export interface InformationState {
    informations: Information[],
    information: InformationViewModel,
    isLoaded: boolean
};

export const initialState: InformationState = {
    informations: null,
    information: null,
    isLoaded: false
};

export function reducer(state: InformationState = initialState, action: InformationActions.Actions) {
    switch (action.type) {

        case InformationActions.GET_INFORMATION:
            return {
                ...state,
                isLoaded: false
            };

        case InformationActions.GET_INFORMATION_SUCCESS:
            return {
                ...state,
                information: action.payload,
                isLoaded: true
            };

        case InformationActions.ADD_INFORMATION:
            return {
                ...state,
                isLoaded: false
            };

        case InformationActions.ADD_INFORMATION_SUCCESS:
            return {
                ...state,
                isLoaded: true
            };

        case InformationActions.UPDATE_INFORMATION:
            return {
                ...state,
                isLoaded: false
            };

        case InformationActions.UPDATE_INFORMATION_SUCCESS:
            return {
                ...state,
                isLoaded: true
            };

        case InformationActions.DELETE_INFORMATION:
            return {
                ...state,
                isLoaded: false
            };

        case InformationActions.DELETE_INFORMATION_SUCCESS:
            return {
                ...state,
                isLoaded: true
            };

        case InformationActions.GET_INFORMATION_LIST:
            return {
                ...state,
                isLoaded: false
            };

        case InformationActions.GET_INFORMATION_LIST_SUCCESS:
            return {
                ...state,
                informations: action.payload,
                isLoaded: true
            };

        default:
            return state;
    }
}
