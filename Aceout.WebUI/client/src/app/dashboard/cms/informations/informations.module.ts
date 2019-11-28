import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AgGridModule } from 'ag-grid-angular';


import { TranslateModule } from '@ngx-translate/core';


import { FontAwesomeModule} from '@fortawesome/angular-fontawesome';
import {library} from '@fortawesome/fontawesome-svg-core';
import { fas } from '@fortawesome/free-solid-svg-icons';
import { far } from '@fortawesome/free-regular-svg-icons';
import { SharedDashboardModule } from 'src/app/shared-dashboard';
import { routes } from './informations.routes';
import { GroupsStoreModule } from 'src/app/store/dashboard/organization/groups/groups.store.module';
import { InformationsComponent } from './informations.component';
import { InformationsEditComponent } from './edit/informations.edit.component';
import { InformationsMainComponent } from './main/informations.main.component';
import { InformationsStoreModule } from 'src/app/store/dashboard/cms/informations/informations.store.module';
import { MatDatepicker, MatDatepickerModule, NativeDateAdapter, DateAdapter, MAT_DATE_FORMATS } from '@angular/material';
import { CommonStoreModule } from 'src/app/store/dashboard/common/common.store.module';

const MY_DATE_FORMATS = {
    parse: {
        dateInput: {month: 'short', year: 'numeric', day: 'numeric'}
    },
    display: {
        // dateInput: { month: 'short', year: 'numeric', day: 'numeric' },
        dateInput: 'input',
        monthYearLabel: {year: 'numeric', month: 'short'},
        dateA11yLabel: {year: 'numeric', month: 'long', day: 'numeric'},
        monthYearA11yLabel: {year: 'numeric', month: 'long'},
    }
 };

 export class MyDateAdapter extends NativeDateAdapter {
    format(date: Date, displayFormat: Object): string {
        if (displayFormat == "input") {
            let day = date.getDate();
            let month = date.getMonth() + 1;
            let year = date.getFullYear();
            return this._to2digit(day) + '.' + this._to2digit(month) + '.' + year;
        } else {
            return date.toDateString();
        }
    }

    private _to2digit(n: number) {
        return ('00' + n).slice(-2);
    }
 }

library.add(fas, far);

@NgModule({
  declarations: [
    InformationsComponent,
    InformationsEditComponent,
    InformationsMainComponent
  ],
  imports: [
    TranslateModule,
    AgGridModule.withComponents([]),
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes),
    FontAwesomeModule,
    SharedDashboardModule,
    GroupsStoreModule,
    InformationsStoreModule,
    MatDatepickerModule,
    CommonStoreModule
  ],
  providers:[
    {provide: DateAdapter, useClass: MyDateAdapter},
    {provide: MAT_DATE_FORMATS, useValue: MY_DATE_FORMATS},
  ]
})
export class InformationsModule {
  public static routes = routes;
}
