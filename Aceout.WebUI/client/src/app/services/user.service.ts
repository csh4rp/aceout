import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({ providedIn: 'root' })
export class UserService {
    constructor() { }
    
    public getUser() : User {
        const service = new JwtHelperService(); 
        const token = service.decodeToken(localStorage.getItem('currentUser'));

        return new User(token.jit, '', token.permissions);
    }
}

export class User{



    constructor(private jit : number, private name : string, private accessPermissions: string[]){

    }

    public get permissions() : string[] { return this.accessPermissions; }

    public get id() : number { return this.jit; }

    public get userName() : string { return this.userName; }
}