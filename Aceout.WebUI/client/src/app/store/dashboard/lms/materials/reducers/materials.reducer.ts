import { MaterialDetails } from "../models/material-details.model";
import * as MaterialActions from "../actions";

export interface MaterialsState {
    material: MaterialDetails,
    isLoaded: boolean
};

const initialState: MaterialsState = {
    material: null,
    isLoaded: false
};


export function reducer(state: MaterialsState = initialState, action: MaterialActions.Actions) {
    switch (action.type) {

        case MaterialActions.GET_MATERIAL:
            return {
                ...state,
                isLoaded: false
            };

        case MaterialActions.GET_MATERIAL_SUCCESS:
            return {
                ...state,
                material: action.payload,
                isLoaded: true
            };


        case MaterialActions.ADD_MATERIAL:
            return {
                ...state,
                material: action.payload,
                isLoaded: false
            };

        case MaterialActions.ADD_MATERIAL_SUCCESS:
            return {
                ...state,
                material: action.payload,
                isLoaded: true
            };

        case MaterialActions.UPDATE_MATERIAL:
            return {
                ...state,
                material: action.payload,
                isLoaded: false
            };

        case MaterialActions.UPDATE_MATERIAL_SUCCESS:
            return {
                ...state,
                material: action.payload,
                isLoaded: true
            };

        case MaterialActions.DELETE_MATERIAL:
            return {
                ...state,
                isLoaded: false
            };

        case MaterialActions.DELETE_MATERIAL_SUCCESS:
            return {
                ...state,
                isLoaded: true
            };

        default:
            return state;
    }
}