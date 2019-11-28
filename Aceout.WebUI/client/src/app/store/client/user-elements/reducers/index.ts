import * as fromElements from './user-elements.reducer';
import { ActionReducerMap, createFeatureSelector } from '@ngrx/store';


export interface ElementsRootState {
    elements: fromElements.UserElementsState;
};

export const reducers : ActionReducerMap<ElementsRootState> = {
    elements: fromElements.reducer
}

export const getElementsRootState = createFeatureSelector<ElementsRootState>('user-elements');