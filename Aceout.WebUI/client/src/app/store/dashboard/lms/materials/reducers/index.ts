import * as fromCategories from './materials.reducer';
import { ActionReducerMap, createFeatureSelector } from '@ngrx/store';


export interface MaterialRootState {
    categories: fromCategories.MaterialsState;
};

export const reducers : ActionReducerMap<MaterialRootState> = {
    categories: fromCategories.reducer
}

export const getMaterialsRootState = createFeatureSelector<MaterialRootState>('materials');