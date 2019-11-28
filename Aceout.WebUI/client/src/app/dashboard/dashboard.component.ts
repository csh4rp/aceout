import { Component, OnInit, ViewEncapsulation } from '@angular/core';

@Component({
  templateUrl: './dashboard.component.html'
})
export class DashboardComponent implements OnInit {

  public ngOnInit() {

  }

  isMenuHidden: boolean;

  menuVisibilityChanged(param: any){
    this.isMenuHidden = param;
  }

}

