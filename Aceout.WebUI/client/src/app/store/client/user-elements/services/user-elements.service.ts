import { Injectable } from "@angular/core";
import { UserElement } from "../models/user-element.model";
import { UrlHelper } from "src/app/app.urls";
import { Observable } from "rxjs";
import { UserElementAnswer } from "../models/user-element-answer.model";
import { HttpClient } from "@angular/common/http";

@Injectable({ providedIn: 'root' })
export class UserElementsService {

    constructor(private http: HttpClient, private urlHelper: UrlHelper) {

    }

    getList(lessonId: number): Observable<UserElement[]> {
        return this.http.get<UserElement[]>(this.urlHelper.getUrl('lms/user-elements/list', lessonId));
    }

    saveElement(element: UserElementAnswer): Observable<UserElementAnswer> {
        return this.http.put<UserElementAnswer>(this.urlHelper.getUrl('lms/user-elements', element.elementId), element);
    }

}