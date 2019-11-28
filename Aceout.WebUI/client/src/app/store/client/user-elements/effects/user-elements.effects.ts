import { Injectable } from "@angular/core";
import { Effect, Actions, ofType } from '@ngrx/effects';
import * as UserElementsActions from '../actions';
import { switchMap, map, tap } from "rxjs/operators";
import { Router } from "@angular/router";
import { UserElementsService } from "../services/user-elements.service";
import { UserElement } from "../models/user-element.model";
import { UserElementAnswer } from "../models/user-element-answer.model";

@Injectable()
export class UserElementsEffects {

    constructor(private actions$: Actions,
        private service: UserElementsService,
        private router: Router) {

    }

    @Effect()
    loadElements$ = this.actions$.pipe(ofType(UserElementsActions.GET_ELEMENT_LIST),
        switchMap((action: UserElementsActions.GetElements) =>
            this.service.getList(action.payload)),
        map((data: UserElement[]) => new UserElementsActions.GetElementsSuccess(data)
        ));

    @Effect()
    saveElement$ = this.actions$.pipe(ofType(UserElementsActions.SAVE_ELEMENT),
        switchMap((action: UserElementsActions.SaveElement) =>
            this.service.saveElement(action.payload)),
        map((data: UserElementAnswer) => new UserElementsActions.SaveElementSuccess(data)
        ));

    @Effect({dispatch: false})
    navigateNext$ = this.actions$.pipe(ofType(UserElementsActions.NAVIGATE_NEXT),
        tap((action: UserElementsActions.NavigateNext) => {
            this.router.navigate(['../' + (action.position + 1)])
        }
        ));

    @Effect({ dispatch: false })
    navigatePrevious$ = this.actions$.pipe(ofType(UserElementsActions.NAVIGATE_PREVIOUS),
        tap((action: UserElementsActions.NavigatePrevious) => {
            this.router.navigate(['../' + (action.position - 1)])
        }
        ));
}