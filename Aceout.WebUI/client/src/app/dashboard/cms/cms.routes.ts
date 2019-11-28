import { Routes } from '@angular/router';


export const routes: Routes = [
    { path: '', loadChildren: './informations#InformationsModule' },
    { path: 'informations', loadChildren: './informations#InformationsModule' },
];
