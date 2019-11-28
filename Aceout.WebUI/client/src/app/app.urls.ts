import { Injectable } from "@angular/core";

@Injectable({providedIn: 'root'})
export class UrlHelper{

    private baseUrl: string = 'http://localhost/aceout/v1.0';

    getUrl(url: string, param?: any, query?: any) : string{
        let apiurl = '';

        if(param){
            apiurl = '/' + param;
        }

        if(query){
            const keys = Object.keys(query);

            apiurl += '?';

            for (let index = 0; index < keys.length; index++) {
                const element = keys[index];
                apiurl += element + '=' + query[element];

                if(index < keys.length - 1){
                    apiurl += '&';
                }
            }
        }

        return this.baseUrl + '/' + url + apiurl;

    }

}
