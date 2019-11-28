import { Component, Input, SimpleChanges, ChangeDetectionStrategy, ChangeDetectorRef } from "@angular/core";
import { FormGroup, FormControl, Validators, FormArray  } from "@angular/forms";
import { ErrorStateMatcher } from "@angular/material";

@Component({
    templateUrl: './dropdown.component.html',
    selector: 'dropdown',
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class DropDownComponent{

    constructor(private changeDetector: ChangeDetectorRef, public errorMatcher: ErrorStateMatcher){
        
    }

    @Input('control')
    control: FormControl;

    @Input('label')
    label: string;

    @Input('required')
    required: boolean;

    @Input('options')
    options: any[];

    get errors() : string[] {
        if(!this.control || !this.control.errors){
            return [];
        }
        const keys = Object.keys(this.control.errors).map(error => {
            switch(error){
                case 'required':
                    return 'Field is required';

                default:
                    return error;
            }
        });

        return keys;
    }

    ngOnChanges(changes: SimpleChanges): void {
        this.changeDetector.detectChanges();
    }

}