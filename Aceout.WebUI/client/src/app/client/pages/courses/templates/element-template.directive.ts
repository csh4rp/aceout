import { ViewContainerRef, Directive } from "@angular/core";

@Directive({
    selector: '[element-template]'
})
export class ElementTemplate{
    constructor(public viewContainerRef: ViewContainerRef){

    }
}