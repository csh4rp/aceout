import { BehaviorSubject, Observable } from 'rxjs';
import { TranslateService } from '@ngx-translate/core';
import { Injectable } from '@angular/core';

@Injectable({providedIn: 'root'})
export class ModalMessageStore{
    private store: BehaviorSubject<ModalMessage>;

    constructor(private translate: TranslateService) {
        this.store = new BehaviorSubject(null);
    }

    addMessage(message: ModalMessage) {
        this.translate.get([message.content, message.title]).subscribe(m => {
            this.store.next(new ModalMessage(m[message.content], m[message.title], message.data, message.action)); 
        })
              
    }

    get messages(): Observable<ModalMessage> {
        return this.store.asObservable();
    }
}

export class ModalMessage{
    constructor(public content: string, public title: string, public data: any, public action: (data: any) => void){

    }
}