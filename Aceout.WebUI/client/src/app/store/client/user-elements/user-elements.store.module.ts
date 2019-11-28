import { NgModule } from "@angular/core";
import { StoreModule } from "@ngrx/store";
import { reducers } from './reducers'
import { EffectsModule } from "@ngrx/effects";
import { effects } from './effects';
import { UserElementsService } from "./services/user-elements.service";

@NgModule({
    declarations: [

    ],
    imports: [
        StoreModule.forFeature('user-elements', reducers),
        EffectsModule.forFeature(effects)
    ]
})
export class UserElementsStoreModule {

}