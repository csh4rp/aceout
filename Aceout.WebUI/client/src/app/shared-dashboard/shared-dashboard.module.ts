import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { MenuComponent } from './menu/menu.component'
import { TopBarComponent } from './top-bar/top-bar.component';
import { MessageComponent } from './message/message.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { library } from '@fortawesome/fontawesome-svg-core';
import { fas } from '@fortawesome/free-solid-svg-icons';
import { far } from '@fortawesome/free-regular-svg-icons';
import { ToolbarItemComponent } from './toolbar/toolbar-item.component';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { HttpLoaderFactory } from 'src/app';
import { HttpClient } from '@angular/common/http';
import { ButtonRowRenderer } from './grid/buttons/button-row-renderer.component';
import { TextBoxComponent } from './forms/textbox/textbox.component';
import {
  MatButtonModule, MatCheckboxModule, ErrorStateMatcher,
  ShowOnDirtyErrorStateMatcher, MatInputModule, MatFormFieldModule,
  MatDialogModule, MatSnackBarModule, MatTabsModule, MatSelectModule, MatAutocompleteModule, MatDatepickerModule, MatNativeDateModule
} from '@angular/material';
import { DialogComponent } from './dialog/dialog.component';
import { PasswordComponent } from './forms/password/password.component';
import { CheckBoxComponent } from './forms/checkbox/checkbox.component';
import { SnackBarComponent } from './snack-bar/snack-bar.component';
import { DropDownComponent } from './forms/dropdown/dropdown.component';
import { CheckboxRowRenderer } from './grid/checkbox/checkbox-row-renderer.component';
import { EditorModule } from '@tinymce/tinymce-angular';
import { FilesComponent } from './files/files.component';
import { FilePickerComponent } from './file-picker/file-picker.component';
import { NumericEditor } from './grid/numeric-editor/numeric-editor.component';

library.add(fas, far);

@NgModule({
  declarations: [
    MenuComponent,
    TopBarComponent,
    MessageComponent,
    ToolbarItemComponent,
    DialogComponent,
    ButtonRowRenderer,
    CheckboxRowRenderer,
    TextBoxComponent,
    PasswordComponent,
    CheckBoxComponent,
    SnackBarComponent,
    DropDownComponent,
    FilesComponent,
    FilePickerComponent,
    NumericEditor
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    FontAwesomeModule,
    TranslateModule,
    MatButtonModule,
    MatCheckboxModule,
    MatInputModule,
    MatFormFieldModule,
    MatSnackBarModule,
    MatDialogModule,
    MatTabsModule,
    MatSelectModule,
    EditorModule,
    MatAutocompleteModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  exports: [
    MenuComponent,
    TopBarComponent,
    MessageComponent,
    ToolbarItemComponent,
    DialogComponent,
    ButtonRowRenderer,
    CheckboxRowRenderer,
    TextBoxComponent,
    PasswordComponent,
    CheckBoxComponent,
    DropDownComponent,
    MatButtonModule,
    MatCheckboxModule,
    MatInputModule,
    MatFormFieldModule,
    MatSnackBarModule,
    MatDialogModule,
    MatTabsModule,
    MatSelectModule,
    EditorModule,
    MatAutocompleteModule,
    MatDatepickerModule,
    MatNativeDateModule,
    FilesComponent,
    FilePickerComponent,
    NumericEditor
  ],
  entryComponents: [
    DialogComponent,
    TextBoxComponent,
    PasswordComponent,
    CheckBoxComponent,
    SnackBarComponent,
    DropDownComponent,
    FilesComponent,
    FilePickerComponent,
    NumericEditor
  ],
  providers: [
    { provide: ErrorStateMatcher, useClass: ShowOnDirtyErrorStateMatcher }
  ]
})
export class SharedDashboardModule {

}
