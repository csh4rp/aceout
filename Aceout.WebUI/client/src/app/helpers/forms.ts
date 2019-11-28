import { FormGroup } from "@angular/forms";

export function assignFormValues(model: any, form: FormGroup){
    const formKeys = Object.keys(form.controls);

    for(let key of formKeys){
        if(model[key] && !(model[key] instanceof Array)){
            form.controls[key].setValue(model[key]);
        }
    }
}