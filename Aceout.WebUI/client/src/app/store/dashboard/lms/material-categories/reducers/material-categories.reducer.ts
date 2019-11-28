import { MaterialCategory } from "../models/material-category.model";
import * as CategoryActions from "../actions";
import { GetMaterialCategory, GetMaterialCategorySuccess, AddMaterialCategory, AddMaterialCategorySuccess } from "../actions";

export interface MaterialCategoriesState {
    category: MaterialCategory,
    categories: MaterialCategory[],
    isLoaded: boolean;
}

const initialState: MaterialCategoriesState = {
    category: null,
    categories: null,
    isLoaded: false
};


export function reducer(state: MaterialCategoriesState = initialState, action: CategoryActions.Actions) {
    switch (action.type) {

        case CategoryActions.GET_MATERIAL_CATEGORY:
            return {
                ...state,
                isLoaded: false
            };
        case CategoryActions.GET_MATERIAL_CATEGORY_SUCCESS:
            return {
                ...state,
                category: action.payload,
                isLoaded: true
            };

        case CategoryActions.ADD_MATERIAL_CATEGORY:
            return {
                ...state,
                isLoaded: false
            };

        case CategoryActions.ADD_MATERIAL_CATEGORY_SUCCESS:
            return {
                ...state,
                category: action.payload,
                isLoaded: true
            };

        case CategoryActions.UPDATE_MATERIAL_CATEGORY:
            return {
                ...state,
                isLoaded: false
            };

        case CategoryActions.UPDATE_MATERIAL_CATEGORY_SUCCESS:
            return {
                ...state,
                category: action.payload,
                isLoaded: true
            };

        case CategoryActions.DELETE_MATERIAL_CATEGORY:
            return {
                ...state,
                isLoaded: false
            };

        case CategoryActions.DELETE_MATERIAL_CATEGORY_SUCCESS:
            return {
                ...state,
                isLoaded: true
            };

        case CategoryActions.GET_MATERIAL_CATEGORY_LIST:
            return {
                ...state,
                isLoaded: false
            };
        case CategoryActions.GET_MATERIAL_CATEGORY_LIST_SUCCESS:
            return {
                ...state,
                categories: action.payload,
                isLoaded: true
            };   

        default:
            return {
                ...state
            };
    }
}
