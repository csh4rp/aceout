import { NgModule } from "@angular/core";
import { StoreModule } from "@ngrx/store";
import { reducers } from './reducers'
import { EffectsModule } from "@ngrx/effects";
import { effects } from './effects';
import { UserLessonsService } from "./services/user-lessons.service";

@NgModule({
    declarations: [

    ],
    imports: [
        StoreModule.forFeature('user-lessons', reducers),
        EffectsModule.forFeature(effects)
    ]
})
export class UserLessonsStoreModule {

}