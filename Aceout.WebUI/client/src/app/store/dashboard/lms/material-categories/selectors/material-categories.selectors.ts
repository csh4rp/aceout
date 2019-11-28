
import * as fromCategories from '../reducers';
import { createSelector } from '@ngrx/store';
import { MaterialCategoriesState } from '../reducers/material-categories.reducer';

export const getMaterialCategoriesState = createSelector(fromCategories.getMaterialCategoriesRootState, 
    (s : fromCategories.MaterialCategoryRootState) => s ? s.categories : undefined);
export const getCategory = createSelector(getMaterialCategoriesState, 
    (s : MaterialCategoriesState) => s ? s.category : undefined);
export const getCategories = createSelector(getMaterialCategoriesState, 
    (s : MaterialCategoriesState) => s ? s.categories : undefined);
export const getCategoryLoaded = createSelector(getMaterialCategoriesState,
    (s: MaterialCategoriesState) => s ? s.isLoaded : undefined);
