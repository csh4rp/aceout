import { NgModule } from "@angular/core";
import { StoreModule } from "@ngrx/store";
import { reducers } from './reducers'
import { EffectsModule } from "@ngrx/effects";
import { effects } from './effects';
import { GroupsService } from "./services/groups.service";

@NgModule({
    declarations: [

    ],
    imports: [
        StoreModule.forFeature('groups', reducers),
        EffectsModule.forFeature(effects)
    ]
})
export class GroupsStoreModule {

}