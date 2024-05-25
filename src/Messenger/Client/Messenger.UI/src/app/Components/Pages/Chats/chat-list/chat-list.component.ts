import { Component, OnInit } from '@angular/core';
import { ChatAPIService } from '../../../../Services/ChatAPIServices/chat-api.service';
import { Chat } from '../../../../Interfaces/Chat/chat';
import { error } from 'console';
import { RouterLink } from '@angular/router';
import { NavbarComponent } from '../../../Navbars/navbar/navbar.component';

@Component({
  selector: 'app-chat-list',
  standalone: true,
  imports: [
    RouterLink,
    NavbarComponent
  ],
  templateUrl: './chat-list.component.html',
  styleUrl: './chat-list.component.scss'
})
export class ChatListComponent implements OnInit{
  pageIndex:number=1;
  chats:Chat[] = []

  constructor(private chatAPIService:ChatAPIService){}

  ngOnInit(): void {
    this.getAllChats();
  }

  getAllChats(){
    this.chatAPIService.getUserChats(this.pageIndex).subscribe(
      (result:Chat[]) =>{
        console.log(result);
        this.chats = result;
      },
      (error) =>{
        console.log("Chatlarni olishda xato ketdi:"+ error.message)
      }
    )
  }
}
