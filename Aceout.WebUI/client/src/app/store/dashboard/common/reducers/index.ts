import * as fromCommon from './common.reducer';
import { ActionReducerMap, createFeatureSelector } from '@ngrx/store';



export interface CommonRootState {
    common: fromCommon.CommonState;
};

export const reducers : ActionReducerMap<CommonRootState> = {
    common: fromCommon.reducer
}
