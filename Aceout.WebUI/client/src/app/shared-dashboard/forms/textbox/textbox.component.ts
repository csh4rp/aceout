import { Component, Input, SimpleChanges, ChangeDetectionStrategy, ChangeDetectorRef, Output, EventEmitter } from "@angular/core";
import { FormGroup, FormControl, Validators, FormArray  } from "@angular/forms";
import { ErrorStateMatcher } from "@angular/material";

@Component({
    templateUrl: './textbox.component.html',
    selector: 'textbox',
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class TextBoxComponent{

    constructor(private changeDetector: ChangeDetectorRef, public errorMatcher: ErrorStateMatcher){
        
    }

    @Input('control')
    control: FormControl;

    @Input('label')
    label: string;

    @Input('required')
    required: boolean;

    @Input('model')
    model: string;

    @Output('model')
    event: EventEmitter<string>;


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