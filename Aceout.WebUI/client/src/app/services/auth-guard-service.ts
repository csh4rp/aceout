import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot } from '@angular/router';
import { UserService } from './user.service';

import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({ providedIn: 'root' })
export class AuthGuardService implements CanActivate {
    constructor(public userService: UserService, public router: Router) { }

    canActivate(route: ActivatedRouteSnapshot): boolean {

        const token = localStorage.getItem('currentUser');

        if (!token) {
            this.router.navigate(['login']);
            return false;
        }

        const helper = new JwtHelperService();
        const isExpired = helper.isTokenExpired(token);

        if (isExpired) {
            this.router.navigate(['login']);
            return false;
        }

        const user = this.userService.getUser();

        if (route.data && route.data.permission) {
            const permission = route.data.permission;

            if (user.permissions.indexOf(permission) != -1) {
                return true;
            }

            return false;
        }
        
        return true;
    }
}