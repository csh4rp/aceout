import { NgModule } from "@angular/core";
import { StoreModule } from "@ngrx/store";
import { reducers } from './reducers'
import { EffectsModule } from "@ngrx/effects";
import { effects } from './effects';
import { RoleService } from "./services/role.service";

@NgModule({
    declarations: [

    ],
    imports: [
        StoreModule.forFeature('roles', reducers),
        EffectsModule.forFeature(effects)
    ]
})
export class RolesStoreModule {

}