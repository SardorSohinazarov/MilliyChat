import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../../Services/auth.service';

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
export class RegisterComponent {
  registerForm: FormGroup = this.formBuilder.group({
    PhoneNumber: ['', Validators.required],
    Password: ['', Validators.required],
    FirstName: ['', Validators.required],
    LastName: [null],
  });

  constructor(
    private formBuilder: FormBuilder,
    private authService:AuthService,
    private router:Router
  ) { }

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

          this.router.navigate(['/chat'])
        },
        error: error => {
          console.error('Xatolik yuz berdi:', error);
        }
      });
  }
}
