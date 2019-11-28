import { Component, OnInit, ViewEncapsulation, ViewChild, Input } from '@angular/core';
import { Location } from '@angular/common';
import { Router } from '@angular/router';
import { UserService, User } from 'src/app/services';
import { trigger, state, style, transition, animate, group } from '@angular/animations';


@Component({
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss'],
  selector: 'dashboard-side-menu'
})
export class MenuComponent {
  private menu: any[] = menuItems;
  private currentPath: string = '/dashboard';
  private user: User;
  private openedItem: string;

  @Input()
  isHidden: boolean;

  constructor(private location: Location, private router: Router, private userService: UserService) {
    this.router.events.subscribe(v => {
      this.currentPath = location.path().replace(new RegExp('/$'), '');
    });

    this.user = userService.getUser();
  }


  public toggleOpened(item: any) {

    if (this.openedItem === item.name) {
      this.openedItem = '';
    }
    else {
      this.openedItem = item.name;
    }
  }

  public isOpened(item: any): boolean {
    return this.openedItem === item.name;
  }

  public isSelected(item: any): boolean {

    let menuPath = null;

    if (item.path != undefined) {
      menuPath = item.path.replace(new RegExp('/$'), '');
    }

    const regex = new RegExp('^' + menuPath + '$');
    if(regex.test(this.currentPath)) {
      return true;
    }

    if (item.children) {
      for (let index = 0; index < item.children.length; index++) {
        const child = item.children[index];

        if (this.isSelected(child)) {
          return true;
        }
      }
    }

    return false;
  }

  public getMenuItems(items: any[]): any[] {

    let filteredItems = [];
    const permissions = this.user.permissions;

    for (let i = 0; i < items.length; i++) {
      let item = items[i];

      if (!item.permission && !item.children) {
        filteredItems.push(item);
      }
      else if (item.permission && !item.children) {
        if (permissions.indexOf(item.permission) != -1) {
          filteredItems.push(item);
        }
      }
      else if (!item.permission && item.children) {
        for (let i = 0; i < item.children.length; i++) {
          let child = item.children[i];

          if (!child.permission || permissions.indexOf(child.permission) != -1) {
            filteredItems.push(item);
            break;
          }
        }
      }
    }

    return filteredItems;
  }


}

const menuItems = [
  {
    name: 'Dashboard',
    path: '/dashboard'
  },
  {
    name: 'Administration',
    children: [
      {
        name: 'Users',
        path: '/dashboard/administration',
        children: [
          {
            name: 'UsersMain',
            path: '/dashboard/administration/users',
          },
          {
            name: 'UsersEdit',
            path: '/dashboard/administration/users/edit/[0-9]',
          }
        ]
      },
      {
        name: 'Roles',
        path: '/dashboard/administration/roles'
      },
    ]
  },
  {
    name: 'LMS',
    children:[
      {
        name: 'Material categories',
        path: '/dashboard/lms/categories'
      },
      {
        name: 'Materials',
        path: '/dashboard/lms/materials'
      },
      {
        name: 'Course Paths',
        path: '/dashboard/lms/course-paths'
      }
      ,
      {
        name: 'Courses',
        path: '/dashboard/lms/courses'
      }
    ]
  },
  {
    name: 'Organization',
    children:[
      {
        name: 'Groups',
        path: '/dashboard/organization/groups'
      },
    ]
  },
  {
    name: 'Cms',
    children:[
      {
        name: 'Informations',
        path: '/dashboard/cms/informations'
      },
    ]
  }
];
