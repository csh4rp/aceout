import { Injectable } from "@angular/core";
import { Effect, Actions, ofType } from '@ngrx/effects';
import * as GroupActions from '../actions';
import { switchMap, tap, map, catchError } from "rxjs/operators";
import { GroupsService } from "../services/groups.service";
import { of } from "rxjs";
import { Store } from "@ngrx/store";
import { Group } from "../models/group.model";
import { MatSnackBar } from "@angular/material";
import { SnackBarComponent } from "src/app/shared-dashboard/snack-bar/snack-bar.component";
import { Location } from "@angular/common";
import * as CommonActions from "src/app/store/dashboard/common/actions";
import { GroupDetails } from "../models/group.details.model";

@Injectable()
export class GroupEffects {

    constructor(private actions$: Actions,
        private groupService: GroupsService) {

    }

    @Effect()
    loadGroup$ = this.actions$.pipe(ofType(GroupActions.GET_GROUP),
        switchMap((action: GroupActions.GetGroup) =>
            this.groupService.getByid(action.payload)),
        map((data: GroupDetails) =>
            new GroupActions.GetGroupSuccess(data)),
        catchError(() =>
            of(new CommonActions.AppError('An error occured while loading group'))
        ));

    @Effect()
    loadGroups$ = this.actions$.pipe(ofType(GroupActions.GET_GROUP_LIST),
        switchMap((action: GroupActions.GetGroupList) =>
            this.groupService.getList()),
        map((data: Group[]) =>
            new GroupActions.GetGroupListSuccess(data)),
        catchError(() =>
            of(new CommonActions.AppError('An error occured while loading groups'))
        ));

    @Effect()
    addGroup$ = this.actions$.pipe(ofType(GroupActions.ADD_GROUP),
        switchMap((action: GroupActions.AddGroup) =>
            this.groupService.add(action.payload)),
        map((data: Group) =>
            new GroupActions.AddGroupSuccess(data)),
        catchError(() =>
            of(new CommonActions.AppError('An error occured while adding group'))
        ));

    @Effect()
    addGroupSuccess$ = this.actions$.pipe(ofType(GroupActions.ADD_GROUP_SUCCESS),
        switchMap((action: GroupActions.AddGroupSuccess) =>
            of(new CommonActions.AppMessage('Group created successfully', ['dashboard', 'organization', 'groups']))));

    @Effect()
    updateGroup$ = this.actions$.pipe(ofType(GroupActions.UPDATE_GROUP),
        switchMap((action: GroupActions.UpdateGroup) =>
            this.groupService.update(action.payload)),
        map((data: Group) =>
            new GroupActions.UpdateGroupSuccess(data)),
        catchError(() =>
            of(new CommonActions.AppError('An error occured while updating group'))));

    @Effect()
    updateUserSuccess$ = this.actions$.pipe(ofType(GroupActions.UPDATE_GROUP_SUCCESS),
        switchMap((action: GroupActions.UpdateGroupSuccess) =>
            of(new CommonActions.AppMessage('Group updated successfully', ['dashboard', 'lms', 'groups']))));

    @Effect()
    deleteUser$ = this.actions$.pipe(ofType(GroupActions.DELETE_GROUP),
        switchMap((action: GroupActions.DeleteGroup) =>
            this.groupService.delete(action.payload)),
        map(() =>
            new GroupActions.DeleteGroupSuccess()),
        catchError(() =>
            of(new CommonActions.AppError('An error occured while deleting group'))));

    @Effect()
    deleteUserSuccess = this.actions$.pipe(ofType(GroupActions.DELETE_GROUP_SUCCESS),
        switchMap((action: GroupActions.DeleteGroupSuccess) =>
            of(new CommonActions.AppMessage('Group deleted successfully'))));

}