import { Injectable } from "@angular/core";
import { Effect, Actions, ofType } from '@ngrx/effects';
import * as InfoActions from '../actions';
import { switchMap, tap, map, catchError } from "rxjs/operators";
import { of } from "rxjs";
import { Store } from "@ngrx/store";
import { MatSnackBar } from "@angular/material";
import { SnackBarComponent } from "src/app/shared-dashboard/snack-bar/snack-bar.component";
import { Location } from "@angular/common";
import * as CommonActions from "src/app/store/dashboard/common/actions";
import { InformationsService } from "../services/informations.service";
import { InformationViewModel } from "../models/information.view-model";
import { InformationDetails } from "../models/information-details.model";
import { Information } from "../models/infotmation.model";


@Injectable()
export class InformationsEffects {

    constructor(private actions$: Actions,
        private service: InformationsService) {

    }

    @Effect()
    loadInformation$ = this.actions$.pipe(ofType(InfoActions.GET_INFORMATION),
        switchMap((action: InfoActions.GetInformation) =>
            this.service.getById(action.payload)),
        map((data: InformationViewModel) =>
            new InfoActions.GetInformationSuccess(data)),
        catchError(() =>
            of(new CommonActions.AppError('An error occured while loading information'))
        ));

    @Effect()
    addInformation$ = this.actions$.pipe(ofType(InfoActions.ADD_INFORMATION),
        switchMap((action: InfoActions.AddInformation) =>
            this.service.add(action.payload)),
        map((data: InformationDetails) =>
            new InfoActions.AddInformationSuccess(data)),
        catchError(() =>
            of(new CommonActions.AppError('An error occured while adding information'))
        ));

    @Effect()
    addInformationSuccess$ = this.actions$.pipe(ofType(InfoActions.ADD_INFORMATION_SUCCESS),
        switchMap((action: InfoActions.AddInformationSuccess) =>
            of(new CommonActions.AppMessage("Information created successfully", ['dashboard', 'cms', 'informations']))));

    @Effect()
    updateInformation$ = this.actions$.pipe(ofType(InfoActions.UPDATE_INFORMATION),
        switchMap((action: InfoActions.UpdateInformation) =>
            this.service.update(action.payload)),
        map((data: InformationDetails) =>
            new InfoActions.UpdateInformationSuccess(data)),
        catchError(() =>
            of(new CommonActions.AppError('An error occured while updating information'))));

    @Effect()
    updateInformationSuccess$ = this.actions$.pipe(ofType(InfoActions.UPDATE_INFORMATION_SUCCESS),
        switchMap((action: InfoActions.UpdateInformationSuccess) =>
            of(new CommonActions.AppMessage("Course updated successfully", ['dashboard', 'cms', 'informations']))));

    @Effect()
    deleteInformation$ = this.actions$.pipe(ofType(InfoActions.DELETE_INFORMATION),
        switchMap((action: InfoActions.DeleteInformation) =>
            this.service.delete(action.payload)),
        map(() =>
            new InfoActions.DeleteInformationSuccess()),
        catchError(() =>
            of(new CommonActions.AppError('An error occured while deleting information'))));

    @Effect()
    deleteInformationSuccess = this.actions$.pipe(ofType(InfoActions.DELETE_INFORMATION_SUCCESS),
        switchMap((action: InfoActions.DeleteInformationSuccess) =>
            of(new CommonActions.AppMessage("Course deleted successfully"))));


    @Effect()
    loadInformations$ = this.actions$.pipe(ofType(InfoActions.GET_INFORMATION_LIST),
        switchMap((action: InfoActions.GetInformationList) =>
            this.service.getList(action.pageNumber, action.count)),
        map((data: Information[]) =>
            new InfoActions.GetInformationListSuccess(data)));
}
