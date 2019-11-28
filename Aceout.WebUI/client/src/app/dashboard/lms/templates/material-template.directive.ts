import { ViewContainerRef, Directive } from "@angular/core";

@Directive({
    selector: '[material-template]'
})
export class MaterialTemplate{
    constructor(public viewContainerRef: ViewContainerRef){

    }
}