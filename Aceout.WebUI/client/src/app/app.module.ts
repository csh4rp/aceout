import { NgModule } from '@angular/core';
import { RouterModule, PreloadAllModules, NoPreloading } from '@angular/router';
import { AppComponent } from './app.component';

import { JwtInterceptor, ErrorInterceptor } from './helpers';

import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';

import { HttpClient, HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';

import { SharedDashboardModule } from './shared-dashboard';
import { RegisterComponent } from './register';
import { ButtonRowRenderer } from './shared-dashboard/grid/buttons/button-row-renderer.component';
import { ROUTES } from './app.routes';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { library } from '@fortawesome/fontawesome-svg-core';
import { fas } from '@fortawesome/free-solid-svg-icons';
import { LoginComponent } from './login';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TextBoxComponent } from './shared-dashboard/forms/textbox/textbox.component';
import { CommonModule } from '@angular/common';
import { BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { CheckboxRowRenderer } from './shared-dashboard/grid/checkbox/checkbox-row-renderer.component';
import { MatDialogModule } from '@angular/material';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import { effects } from './store/dashboard/administration/users/effects';
import { WidgetsModule } from './client/widgets/widgets.module';
import {of} from 'rxjs'
import { ClientModule } from './client';

library.add(fas);

export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http, './assets/langs/i18n/', '.json');
}

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent
  ],
  imports: [
    BrowserAnimationsModule,
    CommonModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    FontAwesomeModule,
    SharedDashboardModule,
    MatDialogModule,
    WidgetsModule,
    ClientModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      }
    }),
    RouterModule.forRoot(ROUTES, {
      useHash: Boolean(history.pushState) === false,
      preloadingStrategy: NoPreloading
    }),
    StoreModule.forRoot({}),
    EffectsModule.forRoot([])
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
  ],
  entryComponents: [
    ButtonRowRenderer,
    CheckboxRowRenderer
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
