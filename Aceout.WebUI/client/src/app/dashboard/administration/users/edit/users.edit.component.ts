import { Component, OnInit, ViewEncapsulation, ChangeDetectorRef } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Pager } from 'src/app/model/pager';
import { FormGroup, FormControl, Validators, FormArray, AbstractControl } from '@angular/forms';
import { FormAssigner } from 'src/app/helpers/formAssigner';
import { Observable, forkJoin, timer, combineLatest } from 'rxjs';
import { NamedFormControl } from 'src/app/controls/namedFormControl';
import { Store } from '@ngrx/store';
import { AppState } from 'src/app/store/app.state';
import * as UserActions from 'src/app/store/dashboard/administration/users/actions';
import { getUserState, getUser } from 'src/app/store/dashboard/administration/users/selectors/users.selectors';
import { User } from 'src/app/store/dashboard/administration/users/models/user.model';
import { Role } from 'src/app/store/dashboard/administration/roles/models/role.model';
import * as RoleActions from 'src/app/store/dashboard/administration/roles/actions';
import { getRoles } from 'src/app/store/dashboard/administration/roles/selectors/roles.selectors';
import { UserService } from 'src/app/store/dashboard/administration/users/services/user.service';
import { map } from 'rxjs/operators';
import { Location } from '@angular/common';
import { assignFormValues } from 'src/app/helpers/forms';



@Component({
  templateUrl: './users.edit.component.html'
})
export class UsersEditComponent implements OnInit {

  dataForm: FormGroup;
  roles$: Observable<Role[]>;
  id: number;
  submitText: string;
  titleText: string;
  userNameText: any;
  errors: string[];
  user$: Observable<User>;
  state$: Observable<any>;

  get form() { return this.dataForm.controls; }
  get roleControls() { return this.dataForm.get('roles') as FormArray; }

  constructor(private userService: UserService,
    private route: ActivatedRoute,
    private changeDetector: ChangeDetectorRef,
    private router: Router,
    private store: Store<AppState>) {
  }

  ngOnInit() {

    this.dataForm = new FormGroup({
      userName: new FormControl('', Validators.required, this.validateUserName.bind(this)),
      email: new FormControl('', Validators.compose([Validators.required, Validators.email])),
      firstName: new FormControl('', Validators.required),
      lastName: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required),
      phoneNumber: new FormControl(''),
      roles: new FormArray([])
    });

    this.roles$ = this.store.select(getRoles);
    this.user$ = this.store.select(getUser);

    this.route.params.subscribe(params => {
      this.id = parseInt(params['id']);

      if (this.id > 0) {
        this.submitText = 'Save changes';
        this.titleText = 'Edit user';

        combineLatest(this.user$, this.roles$).subscribe(([user, roles]) => {
          if (user && roles) {
            assignFormValues(user, this.dataForm);
            this.roleControls.controls.splice(0);

            for (const role of roles) {
              const control = new NamedFormControl(user.userRoles.indexOf(role.id) != -1);
              control.name = role.name;
              control.id = role.id;
              this.roleControls.push(control);
            }
            this.changeDetector.detectChanges();
          }
        });

        this.store.dispatch(new UserActions.GetUser(this.id));
      }
      else {
        this.submitText = 'Add';
        this.titleText = 'Add user';

        this.roles$.subscribe(roles => {

          if (roles) {
            this.roleControls.controls.splice(0);
            for (const role of roles) {
              const control = new NamedFormControl(false);
              control.name = role.name;
              control.id = role.id;
              this.roleControls.push(control);
            }
          }
        });
      }

      this.store.dispatch(new RoleActions.GetRolesList(new Pager(100, 0, "id")));
    });
  }

  saveChanges() {
    const user = this.getUser();

    if (this.id > 0) {
      this.store.dispatch(new UserActions.UpdateUser(user));
    }
    else {
      this.store.dispatch(new UserActions.AddUser(user));
    }
  }

  private getUser(): User {
    const user = this.dataForm.value as User;
    user.userRoles = [];
    user.id = this.id;

    for (const roleControl of this.roleControls.controls) {
      const control = roleControl as NamedFormControl;
      if (control.value === true) {
        user.userRoles.push(control.id);
      }
    }
    return user;
  }



  validateUserName(control: AbstractControl) {
    const converter = map((val: any) => {
      if (val.uniqueUserName === true) {
        return null;
      }
      return val;
    });

    return converter(this.userService.checkUsername(control.value, this.id));
  }

  back() {
    this.router.navigate(['dashboard', 'administration', 'users']);
  }
}

