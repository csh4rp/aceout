import { Component, OnInit, ViewEncapsulation, ViewChild, Output, EventEmitter } from '@angular/core';
import { Location } from '@angular/common';
import { Router } from '@angular/router';
import { UserService, User } from 'src/app/services';
import { ICellRendererAngularComp } from 'ag-grid-angular';

@Component({
  templateUrl: './checkbox-row-renderer.component.html',
  selector: 'checkbox-row-renderer',
  styleUrls: ['./checkbox-row-renderer.component.scss']
})
export class CheckboxRowRenderer implements ICellRendererAngularComp {

  constructor(private router: Router) {

  }


  private params: any;
  private data: any;
  private handler: (status: boolean, data: any) => void;


  isChecked: boolean;

  refresh(params: any): boolean {
    return false;
  }

  agInit(params: any) {
    this.params = params;

    if (params.changeHandler) {
      this.handler = params.changeHandler;
    }

    if (this.params.dataKey) {
      this.data = this.params.data[this.params.dataKey];
    }

    if (this.params.checkedProp) {
      this.isChecked = this.params.data[this.params.checkedProp];
    }

  }

  onChange(param: any) {
    if (this.params.checkedProp) {
      this.params.node.data[this.params.checkedProp] = param.source.checked;
      this.params.setValue(this.params.getValue());
    }

    if (this.handler) {
      this.handler(param.source.checked, this.data);
    }
  }
}
