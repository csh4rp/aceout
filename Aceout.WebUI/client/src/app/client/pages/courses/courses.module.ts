import { NgModule } from "@angular/core";
import { TranslateModule } from "@ngx-translate/core";
import { CommonModule } from "@angular/common";
import { RouterModule } from "@angular/router";
import { UserCoursesStoreModule } from "src/app/store/client/user-courses/user-courses.store.module";
import { CoursesComponent } from "./courses.component";
import { routes } from "./courses.routes";
import { CourseDetailsComponent } from "./course-details/course-details.component";
import { ControlsModule } from "../../controls/controls.module";
import { PipesModule } from "src/app/pipes/pipes.module";
import { LessonDetailsComponent } from "./lesson-details/lesson-details.component";
import { ElementDetailsComponent } from "./element-details/element-details.component";
import { UserElementsStoreModule } from "src/app/store/client/user-elements/user-elements.store.module";
import { UserLessonsStoreModule } from "src/app/store/client/user-lessons/user-lessons.store.module";
import { ElementTemplate } from "./templates/element-template.directive";
import { SingleAnswerComponent } from "./templates/single-answer/single-answer.component";
import { ReactiveFormsModule } from "@angular/forms";

@NgModule({
    declarations: [
        CoursesComponent,
        CourseDetailsComponent,
        LessonDetailsComponent,
        ElementDetailsComponent,
        ElementTemplate,
        SingleAnswerComponent,
    ],
    imports: [
        CommonModule,
        RouterModule.forChild(routes),
        UserCoursesStoreModule,
        ControlsModule,
        TranslateModule,
        PipesModule,
        UserElementsStoreModule,
        UserLessonsStoreModule,
        ReactiveFormsModule
    ],
    entryComponents: [
        SingleAnswerComponent
    ]
})
export class CoursesModule {
    public static routes = routes;
}
