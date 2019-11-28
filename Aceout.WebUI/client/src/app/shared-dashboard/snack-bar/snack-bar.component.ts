import { Component, Inject } from "@angular/core";
import {MAT_SNACK_BAR_DATA} from '@angular/material';

@Component({
    templateUrl: './snack-bar.component.html'
})
export class SnackBarComponent{
    constructor(@Inject(MAT_SNACK_BAR_DATA) public data: any){

    }
}