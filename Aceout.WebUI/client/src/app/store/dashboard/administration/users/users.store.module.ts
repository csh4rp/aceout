import { NgModule } from "@angular/core";
import { StoreModule } from "@ngrx/store";
import { reducers } from './reducers'
import { EffectsModule } from "@ngrx/effects";
import { effects } from './effects';
import { UserService } from "./services/user.service";

@NgModule({
    declarations:[

    ],
    imports:[
        StoreModule.forFeature('users', reducers),
       EffectsModule.forFeature(effects)
    ]
})
export class UsersStoreModule{
    
}