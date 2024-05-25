import { Component, Input } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../Services/AuthServices/auth.service';

@Component({
  selector: 'app-chat-navbar',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './chat-navbar.component.html',
  styleUrl: './chat-navbar.component.scss'
})
export class ChatNavbarComponent {
  @Input({required:true}) chatTitle!:string | null;

  constructor(private authService:AuthService,private router:Router){}

  LogOut(){
    this.authService.logOut();
    this.router.navigate(['/auth/login'])
  }
}
