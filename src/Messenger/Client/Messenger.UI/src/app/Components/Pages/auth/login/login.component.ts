import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControlName, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../../Services/AuthServices/auth.service';
import { UserAPIService } from '../../../../Services/UserAPIServices/user-api.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    RouterLink,
    ReactiveFormsModule,
    FormsModule
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent implements OnInit{
  loginForm: FormGroup = this.formBuilder.group({
    PhoneNumber: ['', Validators.required],
    Password: ['', Validators.required]
  });

  constructor(
    private formBuilder: FormBuilder,
    private authService:AuthService,
    private userService:UserAPIService,
    private router:Router
  ) { }

  ngOnInit(): void {
    if(this.authService.isLoggedIn()){
      this.router.navigate(['/chats'])
    }
  }

  onSubmit(): void {
    if (this.loginForm.invalid) {
      return;
    }

    const loginData = this.loginForm.value;
    
    this.authService.login(loginData)
      .subscribe({
        next: response => {
          console.log(response);

          this.authService.storeToken(response)

          this.userService.getUserInfoFromAPI().subscribe({
            next: userInfo => {
              localStorage.setItem('userProfile', JSON.stringify(userInfo));
              this.router.navigate(['/chats']);
            },
            error: error => {
              console.error('Profil ma\'lumotlarini olishda xato:', error);
            }
          });

          this.router.navigate(['/chats'])
        },
        error: error => {
          console.error('Xatolik yuz berdi:', error);
        }
      });
  }
}
