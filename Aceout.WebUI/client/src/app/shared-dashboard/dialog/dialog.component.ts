import { Component, OnInit, ViewEncapsulation, ViewChild, Output, EventEmitter, Inject } from '@angular/core';
import { Location } from '@angular/common';
import { Router } from '@angular/router';
import { UserService, User } from 'src/app/services';
import { ModalMessage, ModalMessageStore } from 'src/app/model/modalMessageStore';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
  templateUrl: './dialog.component.html',
  styleUrls: ['./dialog.component.scss'],
  selector: 'confirm-dialog'
})
export class DialogComponent {

  message: ModalMessage;
  hasMessage: boolean;

  constructor(private dialogRef: MatDialogRef<DialogComponent>,
     @Inject(MAT_DIALOG_DATA) public data: DialogData) {

  }
}

export class DialogData{
  title: string;
  content: string;
}
