import { Component } from '@angular/core';
import { FormBuilder, FormControlName, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../../Services/auth.service';

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
export class LoginComponent {
  loginForm: FormGroup = this.formBuilder.group({
    PhoneNumber: ['', Validators.required],
    Password: ['', Validators.required]
  });;

  constructor(
    private formBuilder: FormBuilder,
    private authService:AuthService,
    private router:Router
  ) { }

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

          this.router.navigate(['/chat'])
        },
        error: error => {
          console.error('Xatolik yuz berdi:', error);
        }
      });
  }
}
