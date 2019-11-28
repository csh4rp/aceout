import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { UserCoursesStoreModule } from "src/app/store/client/user-courses/user-courses.store.module";
import { BrowserModule } from "@angular/platform-browser";
import { RouterModule } from "@angular/router";
import { CoursesWidgetComponent } from "./courses/courses-widget.component";
import { MenuWidgetComponent } from "./menu/menu-widget-component";
import { TranslateModule } from "@ngx-translate/core";
import { ControlsModule } from "../controls/controls.module";
import { InformationsWidgetComponent } from "./informations/informations-widget.component";
import { InformationsStoreModule } from "src/app/store/dashboard/cms/informations/informations.store.module";


@NgModule({
    declarations: [
        CoursesWidgetComponent,
        MenuWidgetComponent,
        InformationsWidgetComponent
    ],
    imports: [
        ControlsModule,
        CommonModule,
        RouterModule,
        TranslateModule,
        UserCoursesStoreModule,
        InformationsStoreModule
    ],
    exports: [
        CoursesWidgetComponent,
        MenuWidgetComponent,
        InformationsWidgetComponent
    ]
})
export class WidgetsModule {

}
