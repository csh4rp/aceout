import { Component, OnInit, ViewEncapsulation, Input } from '@angular/core';

@Component({
    templateUrl: './image.component.html',
    selector: 'image'
})
export class ImageComponent {

    @Input('width')
    width: number;

    @Input('height')
    height: number;

    @Input('source')
    source: string;

    getUrl(): string {
        let url = this.source;

        if (this.width || this.height) {
            url += '?';
        }

        if (this.width) {
            url += 'width=' + this.width;
            if (this.height) {
                url += '&height' + this.height;
            }
        }
        else if (this.height) {
            url += 'height=' + this.height;
        }

        return url;
    }

}

