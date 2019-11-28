import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { UserCoursesStoreModule } from "src/app/store/client/user-courses/user-courses.store.module";
import { BrowserModule } from "@angular/platform-browser";
import { RouterModule } from "@angular/router";
import { TranslateModule } from "@ngx-translate/core";
import { ImageComponent } from "./image/image.component";


@NgModule({
    declarations:[
        ImageComponent
    ],
    imports:[
    CommonModule,
    RouterModule,
    TranslateModule,
    UserCoursesStoreModule
    ],
    exports:[
        ImageComponent,
    ]
})
export class ControlsModule{

}
