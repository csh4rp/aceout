import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AuthenticationService, UserService, LoginInfo } from '../services';
import { HttpResponse, HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls:['./login.component.scss']
})
export class LoginComponent implements OnInit {

  public loginForm: FormGroup = new FormGroup({
    username: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required)
  });

  public errors: string[];

  constructor(private router: Router, private translate: TranslateService, private authService: AuthenticationService, private userService: UserService) {
  }

  get form() { return this.loginForm.controls; }

  ngOnInit() {

  }

  onSubmit() {

    if(this.loginForm.invalid == true){
      console.log(this.loginForm.errors)
      return;
    }

    this.authService.login(this.form.username.value, this.form.password.value)
      .subscribe((data: LoginInfo) => {

        this.errors = null;
        if(!data.success && data.errors){
          this.errors = data.errors;
          return;
        }

        const user = this.userService.getUser();
        console.log(user);

        if(user && user.permissions){
          this.router.navigate(['/dashboard']);
        }
      });
  }

}
