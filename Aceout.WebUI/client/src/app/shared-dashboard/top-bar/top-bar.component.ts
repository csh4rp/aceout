import { Component, OnInit, ViewEncapsulation, ViewChild, Output, EventEmitter } from '@angular/core';
import { Location } from '@angular/common';
import { Router } from '@angular/router';
import { UserService, User } from 'src/app/services';

@Component({
  templateUrl: './top-bar.component.html',
  styleUrls: ['./top-bar.component.scss'],
  selector: 'dashboard-top-bar'
})
export class TopBarComponent {


  constructor(private location: Location, private router: Router, private userService: UserService) {

  }

  @Output()
  menuVisibilityChanged = new EventEmitter<boolean>();

  public isMenuHidden: boolean;

  public toggleMenu() {
    this.isMenuHidden = !this.isMenuHidden;
    this.menuVisibilityChanged.emit(this.isMenuHidden);
    window.dispatchEvent(new Event('resize'));
  }
}
