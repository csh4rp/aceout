
import * as fromInformations from '../reducers';
import { createSelector } from '@ngrx/store';
import { InformationState } from '../reducers/informations.reducer';

export const getInformationsState = createSelector(fromInformations.getInformationsRootState,
    (s : fromInformations.InformationsRootState) => s ? s.informations : undefined);
export const getInformation = createSelector(getInformationsState, (s : InformationState) => s ? s.information : undefined);
export const getInformationsLoaded = createSelector(getInformationsState, (s: InformationState) => s ? s.isLoaded : undefined);
export const getInformationsList = createSelector(getInformationsState, (s: InformationState) => s ? s.informations : undefined);
