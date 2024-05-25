import { Component, Input, OnInit } from '@angular/core';
import { AuthService } from '../../../Services/AuthServices/auth.service';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})
export class NavbarComponent{
  constructor(private authService:AuthService,private router:Router){}

  LogOut(){
    this.authService.logOut();
    this.router.navigate(['/auth/login'])
  }
}
