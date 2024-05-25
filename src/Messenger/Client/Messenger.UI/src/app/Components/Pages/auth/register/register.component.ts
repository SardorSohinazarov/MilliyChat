import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../../Services/AuthServices/auth.service';
import { UserAPIService } from '../../../../Services/UserAPIServices/user-api.service';
import { Init } from 'node:v8';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [
    RouterLink,
    ReactiveFormsModule,
    FormsModule
  ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup = this.formBuilder.group({
    PhoneNumber: ['', Validators.required],
    Password: ['', Validators.required],
    FirstName: ['', Validators.required],
    LastName: [null],
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
    if (this.registerForm.invalid) {
      return;
    }

    const registerData = this.registerForm.value;
    
    this.authService.register(registerData)
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
