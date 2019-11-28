import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { HttpClient } from '@angular/common/http';
import { UrlHelper } from 'src/app/app.urls';
import { ButtonRowRenderer } from 'src/app/shared-dashboard/grid/buttons/button-row-renderer.component';
import { Router, ActivatedRoute } from '@angular/router';
import { RoleService, RoleModel } from 'src/app/services/dashboard/administration/role.service';
import { FormGroup, FormControl, Validators, FormArray } from '@angular/forms';
import { FormAssigner } from 'src/app/helpers/formAssigner';
import { Observable, forkJoin, combineLatest, } from 'rxjs';
import { NamedFormControl } from 'src/app/controls/namedFormControl';
import { MessageStore, Message } from 'src/app/model/messageStore';
import { share } from 'rxjs/operators';
import { SnackBarComponent } from 'src/app/shared-dashboard/snack-bar/snack-bar.component';
import { MatSnackBar } from '@angular/material';
import { Location } from '@angular/common';
import { RoleDetails } from 'src/app/store/dashboard/administration/roles/models/role-details.model';
import { Store } from '@ngrx/store';
import { AppState } from 'src/app/store/app.state';
import { getRole, getRolesPermissions } from 'src/app/store/dashboard/administration/roles/selectors/roles.selectors';
import { Permission } from 'src/app/store/dashboard/administration/roles/models/permission';
import * as RoleActions from 'src/app/store/dashboard/administration/roles/actions';
import { assignFormValues } from 'src/app/helpers/forms';

@Component({
  templateUrl: './roles.edit.component.html'
})
export class RolesEditComponent implements OnInit {

  private gridApi;
  private gridColumnApi;
  private frameworkComponents;

  dataForm: FormGroup;
  id: number;
  submitText: string;
  titleText: string;
  role$: Observable<RoleDetails>;
  permissions$: Observable<Permission[]>;

  get form() { return this.dataForm.controls; }
  get permissionControls() { return this.dataForm.get('permissions') as FormArray; }

  constructor(private route: ActivatedRoute,
    private router: Router,
    private store: Store<AppState>) {
  }

  ngOnInit() {

    this.dataForm = new FormGroup({
      name: new FormControl('', Validators.required),
      permissions: new FormArray([])
    });

    this.role$ = this.store.select(getRole);
    this.permissions$ = this.store.select(getRolesPermissions);

    this.route.params.subscribe(params => {
      this.id = params['id'];

      if (this.id > 0) {
        this.submitText = 'Save changes';
        this.titleText = 'Edit role';

        this.store.dispatch(new RoleActions.GetRole(this.id));

        combineLatest(this.role$, this.permissions$).subscribe(([role, permissions]) => {
          if (role && permissions) {
            this.permissionControls.controls.splice(0);
            assignFormValues(role, this.dataForm);

            for (const permission of permissions) {
              const control = new NamedFormControl(role.permissions.indexOf(permission.name) != -1);
              control.name = permission.name;
              control.id = permission.id;
              this.permissionControls.push(control);
            }
          }
        });

      }
      else {
        this.submitText = 'Add';
        this.titleText = 'Add role';

        this.permissions$.subscribe(permissions => {
          this.permissionControls.controls.splice(0);

          if (permissions) {
            for (const permission of permissions) {
              const control = new NamedFormControl(false);
              control.name = permission.name;
              control.id = permission.id;
              this.permissionControls.push(control);
            }
          }
        })
      }
    });

    this.store.dispatch(new RoleActions.GetPermissions());

    this.frameworkComponents = {
      editRowRenderer: ButtonRowRenderer
    }
  }

  private getRole(): RoleDetails {
    const role = <RoleDetails>this.dataForm.value;

    role.id = this.id;
    role.permissions = [];

    for (let formControl of this.permissionControls.controls) {
      console.log(this.permissionControls);
      const control = formControl as NamedFormControl;
      if (control.value === true) {
        role.permissions.push(control.name);
      }
    }

    return role;
  }

  onSubmit() {
    const role = this.getRole();

    if (this.id > 0) {
      this.store.dispatch(new RoleActions.UpdateRole(role));
    }
    else {
      this.store.dispatch(new RoleActions.AddRole(role));
    }
  }

  back() {
    this.router.navigate(['dashboard', 'administration', 'roles']);
  }

}



