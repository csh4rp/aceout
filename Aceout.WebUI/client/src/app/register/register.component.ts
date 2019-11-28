import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AuthenticationService, UserService } from '../services';
import { HttpClient } from '@angular/common/http';
import { UrlHelper } from '../app.urls';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls:['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  public registerForm: FormGroup = new FormGroup({
    username: new FormControl('', Validators.required),
    email: new FormControl('', Validators.compose([Validators.required, Validators.email])),
    password: new FormControl('', Validators.required),
    passwordrepeat: new FormControl('', Validators.required)
  });

  constructor(private router: Router,
     private translate: TranslateService,
     private authService: AuthenticationService,
     private userService: UserService,
     private http: HttpClient) {
  }

  get form() { return this.registerForm.controls; }

  ngOnInit() {

  }

  onSubmit() {

    if(!this.registerForm.invalid){
      return;
    }

    let helper = new UrlHelper();
    let username = this.form.username;
    let password = this.form.password;

    this.http.post(helper.getUrl('register'), {username, password}).pipe(r => r);
  }


}
