import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ButtonRowRenderer } from 'src/app/shared-dashboard/grid/buttons/button-row-renderer.component';
import { Router, ActivatedRoute } from '@angular/router';
import { UserService } from 'src/app/services/dashboard/administration/user.service';
import { FormGroup, FormControl, Validators, FormGroupDirective, NgForm } from '@angular/forms';
import { ShowOnDirtyErrorStateMatcher, MatSnackBar } from '@angular/material';
import { Observable } from 'rxjs';
import { User } from 'src/app/model/dashboard/administration/user';
import { Store } from '@ngrx/store';
import { AppState } from 'src/app/store/app.state';
import { getGroup } from 'src/app/store/dashboard/organization/groups/selectors/groups.selectos';
import { Group } from 'src/app/store/dashboard/organization/groups/models/group.model';
import * as GroupActions from 'src/app/store/dashboard/organization/groups/actions';
import { FormAssigner } from 'src/app/helpers/formAssigner';
import { GroupDetails } from 'src/app/store/dashboard/organization/groups/models/group.details.model';
import { GroupUser } from 'src/app/store/dashboard/organization/groups/models/group-user.model';
import { debounceTime, switchMap } from 'rxjs/operators';
import { GridControl } from 'src/app/controls/gridControl';
import { TranslateService } from '@ngx-translate/core';

@Component({
  templateUrl: './groups.edit.component.html'
})
export class GroupsEditComponent extends GridControl implements OnInit {

  dataForm: FormGroup;
  errorMatcher: any;
  frameworkComponents: any;
  columns: any[];
  buttons: any[];
  filteredOptions: User[];
  group$: Observable<GroupDetails>;
  submitText: string;
  titleText: string;

  private id: number;
  private rowData: GroupUser[];
  private gridApi;
  private gridColumnApi;

  constructor(translateService: TranslateService,
    private userService: UserService,
    private route: ActivatedRoute,
    private router: Router,
    private store: Store<AppState>) {
      super(translateService);
  }

  ngOnInit() {
    this.group$ = this.store.select(getGroup);
    this.dataForm = new FormGroup({
      name: new FormControl('', Validators.required),
      userName: new FormControl('', [])
    });

    this.route.params.subscribe(params => {
      this.id = parseInt(params['id']);

      if (this.id > 0) {
        this.submitText = 'Save changes';
        this.titleText = 'Edit group';
        this.store.dispatch(new GroupActions.GetGroup(this.id));
      }
      else {
        this.submitText = 'Add';
        this.titleText = 'Add group'
      }

    });

    this.group$.subscribe(group => {
      if (group) {
        FormAssigner.assign(group, this.dataForm);
        this.rowData = group.users;
      }
    });

    this.buttons = [
      {
        name: 'delete',
        action: p => {
          this.gridApi.forEachNode((node, index) => {
            const item = <User>node.data;
            if (item.id >= p) {
              this.gridApi.updateRowData({ remove: [item] });
            }
          });
        },
        icon: 'trash',
        data: 'id'
      }
    ];

    this.columns = [
      { colId: 'Username', headerName: 'Username', field: 'userName' },
      {
        colId: 'button', cellRenderer: 'buttonRowRenderer', headerName: '', field: 'id',
        width: 90, suppressSizeToFit: true,
        cellRendererParams: { buttons: this.buttons }
      }
    ];

    this.frameworkComponents = {
      buttonRowRenderer: ButtonRowRenderer
    };

    this.dataForm.controls['userName'].valueChanges.pipe(debounceTime(500),
      switchMap((val) =>
        this.userService.autocomplete(val),
      )).subscribe(val => {
        this.filteredOptions = val;
      })

    this.errorMatcher = new ShowOnDirtyErrorStateMatcher();
  }

  addUser() {
    const control = this.dataForm.controls['userName'];

    if (!control.value) {
      control.setErrors({
        empty: true
      });
    }

    this.gridApi.forEachNode((node, index) => {
      const item = node.data as User;

      if (item.id == control.value.id) {
        control.setErrors({
          nonUnique: true
        });
        return;
      }
    });

    if (control.valid) {
      this.gridApi.updateRowData({ add: [control.value] });
      control.setValue('');
    }
  }

  gridReady(params) {
    super.onGridReady(params, this.columns);
    this.gridApi = params.api;
    this.gridColumnApi = params.columnApi;
    this.gridApi.setRowData(this.rowData);
  }

  displayFn(data: User): string | undefined {
    if (!data) return undefined;
    return data.firstName + ' ' + data.lastName + ' [' + data.userName + ']';
  }

  getGroup(): Group {
    const group = <Group>this.dataForm.value;
    group.id = this.id;
    group.userIds = [];

    this.gridApi.forEachNode((node, index) => {
      const item = <User>node.data;
      group.userIds.push(item.id);
    });

    return group;
  }


  onSubmit() {
    const group = this.getGroup();

    if (this.id === 0) {
      this.store.dispatch(new GroupActions.AddGroup(group));
    }
    else {
      this.store.dispatch(new GroupActions.UpdateGroup(group));
    }
  }

  back(){
    this.router.navigate(['dashboard', 'organization', 'groups']);
  }
}


