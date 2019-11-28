import * as fromCategories from './material-categories.reducer';
import { ActionReducerMap, createFeatureSelector } from '@ngrx/store';
import { MaterialCategory } from '../models/material-category.model';


export interface MaterialCategoryRootState {
    categories: fromCategories.MaterialCategoriesState;
};

export const reducers : ActionReducerMap<MaterialCategoryRootState> = {
    categories: fromCategories.reducer
}

export const getMaterialCategoriesRootState = createFeatureSelector<MaterialCategoryRootState>('categories');