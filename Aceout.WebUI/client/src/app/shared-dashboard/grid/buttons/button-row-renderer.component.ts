import { Component, OnInit, ViewEncapsulation, ViewChild, Output, EventEmitter } from '@angular/core';
import { Location } from '@angular/common';
import { Router } from '@angular/router';
import { UserService, User } from 'src/app/services';
import {ICellRendererAngularComp} from 'ag-grid-angular';

@Component({
  templateUrl: './button-row-renderer.component.html',
  selector: 'button-row-renderer',
  styleUrls:['./button-row-renderer.component.scss']
})
export class ButtonRowRenderer implements ICellRendererAngularComp {

constructor(private router: Router){

}

  private redirecter : (param: any) => void;
  private redirecterParameter: string;
  private params : any;
  private buttons: Button[];


  refresh(params: any): boolean {
    return false;
  }

  agInit(params: any){
    this.params = params;

    const buttonArray = this.params.buttons as Array<any>;
    this.buttons = [];

    for(let buttonData of buttonArray){
      let button = new Button(buttonData.name, buttonData.icon, buttonData.action, buttonData.data);
      this.buttons.push(button);
    }
  }

  onClick(name: string){
    let handler = this.buttons.find(b => b.name === name);
    let data = this.params.data[handler.data];
    handler.action(data);
  }
}

class Button{
  constructor(public name: string,
    public icon: string,
    public action : (param: any) => void,
    public data: any){

    }
}
