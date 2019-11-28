import { Component, OnInit, ViewEncapsulation, ViewChild, Output, EventEmitter, Input } from '@angular/core';
import { Location } from '@angular/common';
import { Router } from '@angular/router';
import { UserService, User } from 'src/app/services';
import {ICellRendererAngularComp} from 'ag-grid-angular';
import { TranslateService } from '@ngx-translate/core';
import { Observable } from 'rxjs';

@Component({
  templateUrl: './toolbar-item.component.html',
  selector: 'toolbar-item',
  styleUrls:['./toolbar-item.component.scss']
})
export class ToolbarItemComponent implements OnInit {


constructor(private translate: TranslateService){

}

@Output('click')
btnClick: EventEmitter<any> = new EventEmitter();

@Input('text')
text: string;

@Input('icon')
icon: string;

@Input('color')
color: string;

@Input('type')
type: string;

translatedText: string;

onClick(){
  this.btnClick.emit();
}

ngOnInit(): void {
  this.translate.get(this.text)
    .subscribe(d => this.translatedText = d);
}



}
