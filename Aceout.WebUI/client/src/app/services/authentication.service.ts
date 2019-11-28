import { Injectable } from '@angular/core';
import { HttpClient, HttpRequest, HttpEvent, HttpParams, HttpResponse, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { map, catchError, tap } from 'rxjs/operators';

import { UrlHelperService } from './urlhelper.service';
import { throwError, Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
	constructor(private http: HttpClient, private urlHelper: UrlHelperService) { }

	handleError(errorResponse: HttpErrorResponse) : Observable<LoginInfo>{
		let loginInfo = new LoginInfo(false);
		loginInfo.errors = errorResponse.error.errors;

		return Observable.create(o => o.next(loginInfo));
	}

	login(username: string, password: string) : Observable<LoginInfo> {

		const url = this.urlHelper.getUrl('login');

		return this.http.post<any>(url, { username, password })
			.pipe(map((loginData : any) => {

				let loginInfo = new LoginInfo(true);			
				//login successful if there's a jwt token in the response
				if (loginData && loginData.token) {
					// store user details and jwt token in local storage to keep user logged in between page refreshes
					localStorage.setItem('currentUser', JSON.stringify(loginData));
				}

				return loginInfo;
			}), catchError(this.handleError));
	}

	logout() {
		// remove user from local storage to log user out
		localStorage.removeItem('currentUser');
	}
}

export class LoginInfo{
	errors: string[];

	constructor(public success: boolean){}
}