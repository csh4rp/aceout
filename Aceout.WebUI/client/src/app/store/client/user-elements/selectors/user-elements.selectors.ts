import * as fromElements from '../reducers';
import { createSelector } from '@ngrx/store';


export const getUserElementsState = createSelector(fromElements.getElementsRootState, s => s.elements);
export const getElement = createSelector(getUserElementsState, s => s ? s.selectedElement : undefined);
export const getHasNext = createSelector(getUserElementsState, s => s ? s.hasNext : undefined);
export const getHasPrevious = createSelector(getUserElementsState, s => s ? s.hasPrevious : undefined);
export const getIsSaved = createSelector(getUserElementsState, s => s ? s.isSaved : undefined);



