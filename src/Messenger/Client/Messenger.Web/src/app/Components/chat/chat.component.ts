import { Component, OnInit } from '@angular/core';
import { NgModel } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { error } from 'console';

@Component({
  selector: 'app-chat',
  standalone: true,
  imports: [
    
  ],
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.scss'
})
export class ChatComponent implements OnInit {
  chatId!:string;

  constructor(private activatedRoute:ActivatedRoute){}

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(
      params =>{
        this.chatId = params['chatId'];
        console.log('chatId :' + this.chatId);
      },
      error =>{
        console.log('chatIdni olishda xatolik:',error.message);
      })
  }
}
