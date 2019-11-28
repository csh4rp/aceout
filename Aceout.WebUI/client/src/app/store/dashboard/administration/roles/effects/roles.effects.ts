import { Injectable } from "@angular/core";
import { Effect, Actions, ofType } from '@ngrx/effects';
import * as RoleActions from '../actions';
import { switchMap, tap, map, catchError } from "rxjs/operators";
import { RoleService } from "../services/role.service";
import { of } from "rxjs";
import { Store } from "@ngrx/store";
import { RoleDetails } from "../models/role-details.model";
import { DataSource } from "src/app/model/dataSource";
import * as CommonActions from "src/app/store/dashboard/common/actions";
import { Permission } from "../models/permission";

@Injectable()
export class RolesEffects {


    constructor(private actions$: Actions,
        private roleService: RoleService) {

    }

    @Effect()
    loadRoles$ = this.actions$.pipe(ofType(RoleActions.GET_ROLES_LIST),
        switchMap((action: RoleActions.GetRolesList) =>
            this.roleService.getData(action.payload)),
        map((data: DataSource<RoleDetails>) =>
            new RoleActions.GetRolesListSuccess(data.data)),
        catchError(() =>
            of(new CommonActions.AppError('An error occured while loading roles')))
    );

    @Effect()
    loadRole$ = this.actions$.pipe(ofType(RoleActions.GET_ROLE),
        switchMap((action: RoleActions.GetRole) =>
            this.roleService.getById(action.payload)),
        map((data: RoleDetails) =>
            new RoleActions.GetRoleSuccess(data)),
        catchError(() =>
            of(new CommonActions.AppError('An error occured while loading role')))
    );

    @Effect()
    addRole$ = this.actions$.pipe(ofType(RoleActions.ADD_ROLE),
        switchMap((action: RoleActions.AddRole) =>
            this.roleService.add(action.payload)),
        map((data: RoleDetails) =>
            new RoleActions.AddRoleSuccess(data)),
        catchError(() =>
            of(new CommonActions.AppError('An error occured while adding role'))));


    @Effect()
    addRoleSuccess$ = this.actions$.pipe(ofType(RoleActions.ADD_ROLE_SUCCESS),
        switchMap((action: RoleActions.AddRoleSuccess) =>
            of(new CommonActions.AppMessage("Role created successfully", ['dashboard', 'administration', 'roles']))));

    @Effect()
    updateRole$ = this.actions$.pipe(ofType(RoleActions.UPDATE_ROLE),
        switchMap((action: RoleActions.UpdateRole) =>
            this.roleService.update(action.payload)),
        map((data: RoleDetails) =>
            new RoleActions.UpdateRoleSuccess(data)),
        catchError(() =>
            of(new CommonActions.AppError('An error occured while updating role')))
    );

    @Effect()
    updateRoleSuccess$ = this.actions$.pipe(ofType(RoleActions.UPDATE_ROLE_SUCCESS),
        switchMap((action: RoleActions.UpdateRoleSuccess) =>
            of(new CommonActions.AppMessage("Role updated successfully", ['dashboard', 'administration', 'roles']))));

    @Effect()
    deleteRole$ = this.actions$.pipe(ofType(RoleActions.DELETE_ROLE),
        switchMap((action: RoleActions.DeleteRole) =>
            this.roleService.delete(action.payload)),
        map((data: RoleDetails) =>
            new RoleActions.DeleteRoleSuccess()),
        catchError(() =>
            of(new CommonActions.AppError('An error occured while deleting role'))));

    @Effect()
    deleteRoleSuccess$ = this.actions$.pipe(ofType(RoleActions.DELETE_ROLE_SUCCESS),
        switchMap((action: RoleActions.DeleteRoleSuccess) =>
            of(new CommonActions.AppMessage("Role deleted successfully"))));


    @Effect()
    loadPermissions$ = this.actions$.pipe(ofType(RoleActions.GET_PERMISSIONS),
        switchMap((action: RoleActions.GetPermissions) =>
            this.roleService.getPermissions()),
        map((data: Permission[]) =>
            new RoleActions.GetPermissionsSuccess(data)),
        catchError(() =>
            of(new CommonActions.AppError('An error occured while loading permissions')))
    );

}
