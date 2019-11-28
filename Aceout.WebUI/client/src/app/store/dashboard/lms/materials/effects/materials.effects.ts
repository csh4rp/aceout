import { Injectable } from "@angular/core";
import { Effect, Actions, ofType } from '@ngrx/effects';
import * as MaterialActions from '../actions';
import { switchMap, tap, map, catchError } from "rxjs/operators";
import { of } from "rxjs";
import * as CommonActions from "src/app/store/dashboard/common/actions";
import { MaterialsService } from "../services/materials.service";
import { MaterialDetails } from "../models/material-details.model";

@Injectable()
export class MaterialsEffects {

    constructor(private actions$: Actions,
        private categoriesService: MaterialsService){
     
    }

    @Effect()
    loadmaterial$ = this.actions$.pipe(ofType(MaterialActions.GET_MATERIAL),
        switchMap((action: MaterialActions.GetMaterial) =>
            this.categoriesService.getById(action.payload)),
        map((data: MaterialDetails) =>
            new MaterialActions.GetMaterialSuccess(data)),
        catchError(() =>
            of(new CommonActions.AppError('An error occured while loading material'))
        ));

    @Effect()
    addMaterial$ = this.actions$.pipe(ofType(MaterialActions.ADD_MATERIAL),
        switchMap((action: MaterialActions.AddMaterial) => 
            this.categoriesService.add(action.payload)),
        map((data: MaterialDetails) =>
            new MaterialActions.AddMaterialSuccess(data)),
        catchError(() =>
            of(new CommonActions.AppError('An error occured while adding material'))
        ));

    @Effect()
    addMaterialSuccess$ = this.actions$.pipe(ofType(MaterialActions.ADD_MATERIAL_SUCCESS),
        switchMap((action: MaterialActions.AddMaterialSuccess) =>
            of(new CommonActions.AppMessage("Material created successfully", ['dashboard', 'lms', 'materials']))));

    @Effect()
    updateMaterial$ = this.actions$.pipe(ofType(MaterialActions.UPDATE_MATERIAL),
        switchMap((action: MaterialActions.UpdateMaterial) =>
            this.categoriesService.update(action.payload)),
        map((data: MaterialDetails) =>
            new MaterialActions.UpdateMaterialSuccess(data)),
        catchError(() =>
            of(new CommonActions.AppError('An error occured while updating material'))));

    @Effect()
    updateMaterialSuccess$ = this.actions$.pipe(ofType(MaterialActions.UPDATE_MATERIAL_SUCCESS),
        switchMap((action: MaterialActions.UpdateMaterialSuccess) =>
            of(new CommonActions.AppMessage("Material updated successfully", ['dashboard', 'lms', 'materials']))));

    @Effect()
    deleteMaterial$ = this.actions$.pipe(ofType(MaterialActions.DELETE_MATERIAL),
        switchMap((action: MaterialActions.DeleteMaterial) =>
            this.categoriesService.delete(action.payload)),
        map(() =>
            new MaterialActions.DeleteMaterialSuccess()),
        catchError(() =>
            of(new CommonActions.AppError('An error occured while deleting material'))));

    @Effect()
    deleteMaterialSuccess = this.actions$.pipe(ofType(MaterialActions.DELETE_MATERIAL_SUCCESS),
        switchMap((action: MaterialActions.DeleteMaterialSuccess) =>
            of(new CommonActions.AppMessage("Material deleted successfully"))));

}