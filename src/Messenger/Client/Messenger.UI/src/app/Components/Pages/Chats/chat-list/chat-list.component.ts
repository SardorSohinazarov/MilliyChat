import { Component, OnInit } from '@angular/core';
import { ChatAPIService } from '../../../../Services/ChatAPIServices/chat-api.service';
import { Chat } from '../../../../Interfaces/chat';
import { error } from 'console';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-chat-list',
  standalone: true,
  imports: [
    RouterLink
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
