import { Injectable } from "@angular/core";
import { Effect, Actions, ofType } from '@ngrx/effects';
import * as UserActions from '../actions';
import { switchMap, tap, map, catchError } from "rxjs/operators";
import { UserService } from "../services/user.service";
import { of } from "rxjs";
import { Store } from "@ngrx/store";
import { User } from "../models/user.model";
import { MatSnackBar } from "@angular/material";
import { SnackBarComponent } from "src/app/shared-dashboard/snack-bar/snack-bar.component";
import { Location } from "@angular/common";
import * as CommonActions from "src/app/store/dashboard/common/actions";

@Injectable()
export class UsersEffects {

    constructor(private actions$: Actions,
        private userService: UserService,
        private snackBar: MatSnackBar,
        private location: Location){
     
    }

    @Effect()
    loadUser$ = this.actions$.pipe(ofType(UserActions.GET_USER),
        switchMap((action: UserActions.GetUser) =>
            this.userService.getById(action.payload)),
        map((data: User) =>
            new UserActions.GetUsersSuccess(data)),
        catchError(() =>
            of(new CommonActions.AppError('An error occured while loading user'))
        ));

    @Effect()
    addUser$ = this.actions$.pipe(ofType(UserActions.ADD_USER),
        switchMap((action: UserActions.AddUser) =>
            this.userService.add(action.payload)),
        map((data: User) =>
            new UserActions.AddUserSuccess(data)),
        catchError(() =>
            of(new CommonActions.AppError('An error occured while adding user'))
        ));

    @Effect()
    addUserSuccess$ = this.actions$.pipe(ofType(UserActions.ADD_USER_SUCCESS),
        switchMap((action: UserActions.AddUserSuccess) =>
            of(new CommonActions.AppMessage("User created successfully", ['dashboard', 'administration', 'users']))));

    @Effect()
    updateUser$ = this.actions$.pipe(ofType(UserActions.UPDATE_USER),
        switchMap((action: UserActions.UpdateUser) =>
            this.userService.update(action.payload)),
        map((data: User) =>
            new UserActions.UpdateUserSuccess(data)),
        catchError(() =>
            of(new CommonActions.AppError('An error occured while updating user'))));

    @Effect()
    updateUserSuccess$ = this.actions$.pipe(ofType(UserActions.UPDATE_USER_SUCCESS),
        switchMap((action: UserActions.UpdateUserSuccess) =>
            of(new CommonActions.AppMessage("User updated successfully", ['dashboard', 'administration', 'users']))));

    @Effect()
    deleteUser$ = this.actions$.pipe(ofType(UserActions.DELETE_USER),
        switchMap((action: UserActions.DeleteUser) =>
            this.userService.delete(action.payload)),
        map(() =>
            new UserActions.DeleteUserSuccess()),
        catchError(() =>
            of(new CommonActions.AppError('An error occured while deleting user'))));

    @Effect()
    deleteUserSuccess = this.actions$.pipe(ofType(UserActions.DELETE_USER_SUCCESS),
        switchMap((action: UserActions.DeleteUserSuccess) =>
            of(new CommonActions.AppMessage("User deleted successfully"))));

}