import * as fromInformations from './informations.reducer';
import { ActionReducerMap, createFeatureSelector } from '@ngrx/store';



export interface InformationsRootState {
    informations: fromInformations.InformationState;
};

export const reducers : ActionReducerMap<InformationsRootState> = {
    informations: fromInformations.reducer
}

export const getInformationsRootState = createFeatureSelector<InformationsRootState>('informations');
