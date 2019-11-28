
import * as fromMaterials from '../reducers';
import { createSelector } from '@ngrx/store';
import { MaterialsState } from '../reducers/materials.reducer';

export const getMaterialsState = createSelector(fromMaterials.getMaterialsRootState, 
    (s : fromMaterials.MaterialRootState) => s ? s.categories : undefined);
export const getMaterial = createSelector(getMaterialsState, (s : MaterialsState) => s ? s.material : undefined);
export const getMaterialsLoaded = createSelector(getMaterialsState, (s: MaterialsState) => s ? s.isLoaded : undefined);
