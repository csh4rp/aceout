import { Injectable } from "@angular/core";
import { Pager } from "src/app/model/pager";
import { Observable } from "rxjs";
import { DataSource } from "src/app/model/dataSource";
import { HttpClient } from "@angular/common/http";
import { UrlHelper } from "src/app/app.urls";
import { UserLessonDetails } from "../models/user-lesson-details.model";

@Injectable({ providedIn: 'root' })
export class UserLessonsService {

    constructor(private http: HttpClient, private urlHelper: UrlHelper) {

    }

    getById(id: number): Observable<UserLessonDetails> {
        return this.http.get<UserLessonDetails>(this.urlHelper.getUrl('lms/user-lessons', id));
    }

    startAttempt(id: number): Observable<any> {
        return this.http.post(this.urlHelper.getUrl('lms/user-lessons/start-attempt'), { lessonId: id });
    }

    finishAttempt(id: number): Observable<any> {
        return this.http.post(this.urlHelper.getUrl('lms/user-lessons/finish-attempt'), { lessonId: id })
    }
}
