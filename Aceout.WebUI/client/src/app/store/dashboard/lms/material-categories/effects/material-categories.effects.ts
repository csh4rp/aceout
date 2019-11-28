import { Injectable } from "@angular/core";
import { Effect, Actions, ofType } from '@ngrx/effects';
import * as CategoryActions from '../actions';
import { switchMap, tap, map, catchError } from "rxjs/operators";
import { MaterialCategoriesService } from "../services/material-categories.service";
import { of } from "rxjs";
import { Store } from "@ngrx/store";
import { MaterialCategory } from "../models/material-category.model";
import { MatSnackBar } from "@angular/material";
import { SnackBarComponent } from "src/app/shared-dashboard/snack-bar/snack-bar.component";
import { Location } from "@angular/common";
import * as CommonActions from "src/app/store/dashboard/common/actions";

@Injectable()
export class MaterialCategoriesEffects {

    constructor(private actions$: Actions,
        private categoriesService: MaterialCategoriesService) {

    }

    @Effect()
    loadCategory$ = this.actions$.pipe(ofType(CategoryActions.GET_MATERIAL_CATEGORY),
        switchMap((action: CategoryActions.GetMaterialCategory) =>
            this.categoriesService.getById(action.payload)),
        map((data: MaterialCategory) =>
            new CategoryActions.GetMaterialCategorySuccess(data)),
        catchError(() =>
            of(new CommonActions.AppError('An error occured while loading material category'))
        ));

    @Effect()
    loadList$ = this.actions$.pipe(ofType(CategoryActions.GET_MATERIAL_CATEGORY_LIST),
        switchMap((action: CategoryActions.GetMaterialCategoryList) =>
            this.categoriesService.getList()),
        map((data: MaterialCategory[]) =>
            new CategoryActions.GetMaterialCategoryListSuccess(data)),
        catchError(() =>
            of(new CommonActions.AppError('An error occured while loading material categories'))
        ));

    @Effect()
    addCategory$ = this.actions$.pipe(ofType(CategoryActions.ADD_MATERIAL_CATEGORY),
        switchMap((action: CategoryActions.AddMaterialCategory) =>
            this.categoriesService.add(action.payload)),
        map((data: MaterialCategory) =>
            new CategoryActions.AddMaterialCategorySuccess(data)),
        catchError(() =>
            of(new CommonActions.AppError('An error occured while adding material category'))
        ));

    @Effect()
    addCategorySuccess$ = this.actions$.pipe(ofType(CategoryActions.ADD_MATERIAL_CATEGORY_SUCCESS),
        switchMap((action: CategoryActions.AddMaterialCategorySuccess) =>
            of(new CommonActions.AppMessage("Material category created successfully", ['dashboard', 'lms', 'material-categories']))));

    @Effect()
    updateCategory$ = this.actions$.pipe(ofType(CategoryActions.UPDATE_MATERIAL_CATEGORY),
        switchMap((action: CategoryActions.UpdateMaterialCategory) =>
            this.categoriesService.update(action.payload)),
        map((data: MaterialCategory) =>
            new CategoryActions.UpdateMaterialCategorySuccess(data)),
        catchError(() =>
            of(new CommonActions.AppError('An error occured while updating material category'))));

    @Effect()
    updateCategorySuccess$ = this.actions$.pipe(ofType(CategoryActions.UPDATE_MATERIAL_CATEGORY_SUCCESS),
        switchMap((action: CategoryActions.UpdateMaterialCategorySuccess) =>
            of(new CommonActions.AppMessage("Material category updated successfully", ['dashboard', 'lms', 'material-categories']))));

    @Effect()
    deleteUser$ = this.actions$.pipe(ofType(CategoryActions.DELETE_MATERIAL_CATEGORY),
        switchMap((action: CategoryActions.DeleteMaterialCategory) =>
            this.categoriesService.delete(action.payload)),
        map(() =>
            new CategoryActions.DeleteMaterialCategorySuccess()),
        catchError(() =>
            of(new CommonActions.AppError('An error occured while deleting material category'))));

    @Effect()
    deleteUserSuccess = this.actions$.pipe(ofType(CategoryActions.DELETE_MATERIAL_CATEGORY_SUCCESS),
        switchMap((action: CategoryActions.DeleteMaterialCategorySuccess) =>
            of(new CommonActions.AppMessage("Material category deleted successfully"))));

}