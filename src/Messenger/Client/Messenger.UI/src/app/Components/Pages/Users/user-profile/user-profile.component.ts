import { Component, OnInit } from '@angular/core';
import { UserAPIService } from '../../../../Services/UserAPIServices/user-api.service';
import { UserProfileViewModel } from '../../../../Interfaces/Users/user-profile-view-model';
import { error } from 'console';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { ChatAPIService } from '../../../../Services/ChatAPIServices/chat-api.service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-user-profile',
  standalone: true,
  imports: [
    DatePipe,
    RouterLink
  ],
  templateUrl: './user-profile.component.html',
  styleUrl: './user-profile.component.scss'
})
export class UserProfileComponent implements OnInit {
  userProfile!:UserProfileViewModel;
  userId!:number;

  constructor(
    private userAPIService:UserAPIService,
    private activatedRoute:ActivatedRoute,
    private chatAPIService:ChatAPIService,
    private router:Router
  ){ }

  ngOnInit(): void {
    this.userId = this.activatedRoute.snapshot.params['userId'];

    this.getUserProfile(this.userId);
  }

  getUserProfile(userId:number){
    this.userAPIService.getUserProfile(userId).subscribe(
      (result:UserProfileViewModel) =>{
        console.log('Profile:' + result);
        this.userProfile = result;
      },
      error =>{
        console.log(error.message)
      }
    )
  }

  message(){
    this.chatAPIService.getOrCreateJoinChat(this.userId).subscribe(
      (result:string)=>{
        this.router.navigate([`/chats/${result}`])
      },
      error =>{
        console.log(error.message)
      }
    )
  }
}
