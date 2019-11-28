import { Component, OnInit, ViewEncapsulation, ViewChild, Output, EventEmitter, Inject } from '@angular/core';
import { Location } from '@angular/common';
import { Router } from '@angular/router';
import { UserService, User } from 'src/app/services';
import { ModalMessage, ModalMessageStore } from 'src/app/model/modalMessageStore';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
  templateUrl: './files.component.html',
  styleUrls: ['./files.component.scss'],
  selector: 'files-dialog'
})
export class FilesComponent {


  constructor(){
  }
}

