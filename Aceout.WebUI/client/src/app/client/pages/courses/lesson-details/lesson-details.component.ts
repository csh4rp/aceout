import { Component, OnInit } from "@angular/core";
import { Store } from "@ngrx/store";
import { AppState } from "src/app/store/app.state";
import { Observable } from "rxjs";
import { UserLessonDetails } from "src/app/store/client/user-lessons/models/user-lesson-details.model";
import * as UserLessonSelectors from 'src/app/store/client/user-lessons/selectors/user-lessons.selectors';
import * as UserLessonActions from 'src/app/store/client/user-lessons/actions/user-lessons.actions';
import { ActivatedRoute, Router } from "@angular/router";

@Component({
    templateUrl: './lesson-details.component.html'
})
export class LessonDetailsComponent implements OnInit {

    private lessonId: number;
    private courseId: number;
    lesson$: Observable<UserLessonDetails>;

    constructor(private store: Store<AppState>,
        private route: ActivatedRoute,
        private router: Router) {

    }

    ngOnInit() {
        this.lesson$ = this.store.select(UserLessonSelectors.getLesson);

        this.route.params.subscribe(p => {
            this.courseId = parseInt(p['id']);
            this.lessonId = parseInt(p['lessonId']);
            this.store.dispatch(new UserLessonActions.GetLesson(this.lessonId));
        });
    }

    startLesson() {
        this.store.dispatch(new UserLessonActions.StartLesson(this.lessonId));
    }

    continueLesson() {
        this.router.navigate(['courses', this.courseId, this.lessonId, 0]);
    }

    checkAnswers() {
        this.router.navigate(['courses', this.courseId, this.lessonId, 0], { queryParams: { mode: 'preview' } });
    }

    getLessonAttempt(lesson: UserLessonDetails): string {

        let attempt = lesson.attempt ? lesson.attempt.toString() : '-';

        if (lesson.attemptLimit) {
            return attempt + '/' + lesson.attemptLimit.toString();
        }
        else if (!lesson.attemptLimit) {
            return attempt;
        }
    }

}
