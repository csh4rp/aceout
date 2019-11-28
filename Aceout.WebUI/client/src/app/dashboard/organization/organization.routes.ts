import { Routes } from '@angular/router';


export const routes: Routes = [
 { path: '', loadChildren: './groups#GroupsModule'},
 { path: 'groups', loadChildren: './groups#GroupsModule'},
];
