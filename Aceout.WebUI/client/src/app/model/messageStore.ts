import { Injectable } from '@angular/core';
import { Observable, BehaviorSubject } from 'rxjs';
import { TranslateService } from '@ngx-translate/core';

@Injectable({ providedIn: 'root' })
export class MessageStore {

    private store: BehaviorSubject<Message>;

    constructor(private translate: TranslateService) {
        this.store = new BehaviorSubject(null);
    }

    addMessage(message: Message) {
        this.translate.get(message.content).subscribe(m => {
            this.store.next(new Message(m, message.status)); 
        })
              
    }

    get messages(): Observable<Message> {
        return this.store.asObservable();
    }
}

export class Message {

    constructor(public content: string, public status: string) {

    }
}