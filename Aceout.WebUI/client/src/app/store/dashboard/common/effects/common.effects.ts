import { Injectable } from "@angular/core";
import { Effect, Actions, ofType } from '@ngrx/effects';
import * as CommonActions from '../actions';
import { switchMap, tap, map, catchError } from "rxjs/operators";
import { of } from "rxjs";
import { Store } from "@ngrx/store";
import { MatSnackBar } from "@angular/material";
import { SnackBarComponent } from "src/app/shared-dashboard/snack-bar/snack-bar.component";
import { Location } from "@angular/common";
import { Router } from "@angular/router";

@Injectable()
export class CommonEffects {


    constructor(private actions$: Actions,
        private snackBar: MatSnackBar,
        private location: Location,
        private router: Router
    ) {

    }

    @Effect({ dispatch: false })
    appError$ = this.actions$.pipe(ofType(CommonActions.APP_ERROR), tap((action: CommonActions.AppError) => {
        this.snackBar.openFromComponent(SnackBarComponent, {
            data: {
                text: action.payload
            }
        });
    }));

    @Effect({ dispatch: false })
    appMessage$ = this.actions$.pipe(ofType(CommonActions.APP_MESSAGE), tap((action: CommonActions.AppMessage) => {
        this.snackBar.openFromComponent(SnackBarComponent, {
            data: {
                text: action.payload
            }
        });
        if (action.redirectUrl) {
            this.router.navigate(action.redirectUrl);
        }
    }));

}