import { Injectable } from '@angular/core';


@Injectable({ providedIn: 'root' })
export class UrlHelperService{

    public getUrl(path: string) : string{
        return 'https://aceout.azurewebsites.net/v1.0' + '/' + path;
    }
}
