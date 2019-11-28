import { Component, OnInit, ViewEncapsulation, ViewChild, Output, EventEmitter } from '@angular/core';
import { Location } from '@angular/common';
import { Router } from '@angular/router';
import { UserService, User } from 'src/app/services';
import { MessageStore } from 'src/app/model/messageStore';

@Component({
  templateUrl: './message.component.html',
  styleUrls: ['./message.component.scss'],
  selector: 'panel-messages'
})
export class MessageComponent implements OnInit {

  message: string;
  status: string;
  hasMessage: boolean;

  constructor(private messageStore: MessageStore) {

  }

  ngOnInit(): void {
    this.messageStore.messages.subscribe(m => {
      if (m) {
        this.message = m.content;
        this.status = 'alert-' + m.status;
        this.hasMessage = true;
      }
    });
  }

  hide(): void {
    this.hasMessage = false;
  }

  getClass(): string {
    if (this.hasMessage) {
      return this.status;
    }

    return "hide";
  }

}
