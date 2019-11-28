import { Component, OnInit, ViewEncapsulation } from '@angular/core';

@Component({
  templateUrl: './client.component.html'
})
export class ClientComponent implements OnInit {

  public ngOnInit() {

  }

  isMenuHidden: boolean;

  menuVisibilityChanged(param: any){
    this.isMenuHidden = param;
  }

}

