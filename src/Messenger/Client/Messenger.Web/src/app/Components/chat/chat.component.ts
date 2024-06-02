import { Component, OnDestroy, OnInit } from '@angular/core';
import { NgModel } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import { error } from 'console';
import { MessageDTO } from '../../Models/Messages/message-dto';
import { Chat } from '../../Models/Chats/chat';
import { MessageService } from '../../Services/Messages/message.service';
import { ChatService } from '../../Services/Chats/chat.service';
import { AuthService } from '../../Services/Auth/auth.service';
import { UserViewModel } from '../../Models/Users/user-view-model';
import { MessageCreationDTO } from '../../Models/Messages/message-creation-dto';

@Component({
  selector: 'app-chat',
  standalone: true,
  imports: [
    
  ],
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.scss'
})
export class ChatComponent implements OnInit, OnDestroy{
  private connection: HubConnection;
  public messages: MessageDTO[] = [];
  public inputText: string = '';
  private chatId: string = '';
  public userInfo: UserViewModel | null = null;
  public chat!:Chat;

  constructor(
    private activatedRoute: ActivatedRoute,
    private messageAPIService: MessageService,
    private chatAPIService:ChatService,
    private authService: AuthService
  ) {
    this.connection = new HubConnectionBuilder()
      .withUrl('https://localhost:7031/chathub',
        {
          accessTokenFactory: () => this.authService.getAccessToken()!
        })
      .configureLogging(LogLevel.Information)
      .build();
  }
  ngOnDestroy(): void {
    this.connection.stop();
    console.log('Connection closed to SignalR hub');
  }

  async ngOnInit() {
    this.activatedRoute.params.subscribe(
      params =>{
        if (params && params['chatId']) {
          this.chatId = params['chatId'];
          console.log('chatId :' + this.chatId);
          this.loadUser();
          this.getChat();
          this.getMessages();
        } else {
          console.error('Chat ID not provided!');
        }
  
      },
      error =>{
        console.log('chatIdni olishda xatolik:',error.message);
      })

    this.connection.on('ReceiveMessage', (message) => {
      this.messages.push(message);

      this.scrollFunction();
    });

    try {
      await this.connection.start();
      console.log('Connected to SignalR hub');
    } catch (error) {
      console.error('Failed to connect to SignalR hub', error);
    }
  }

  scrollFunction() {
    const item = document.querySelector("body") as HTMLElement;

    setTimeout(() => window.scrollTo(0,item.scrollHeight + 30),1
    );
  }

  loadUser(): void {
    const serializedData = localStorage.getItem('userProfile');
    if (serializedData) {
      this.userInfo = JSON.parse(serializedData) as UserViewModel;
      console.log(this.userInfo);
    }
  }

  getMessages(){
    this.messageAPIService.getChatMessages(this.chatId).subscribe(
      (messages: MessageDTO[]) => {
        console.log(messages);
        this.messages = messages;
      },
      error =>{
        console.log('Messagelarni yuklashda xatolik:' + error.message)
      }
    );
  }

  getChat(){
    this.chatAPIService.getChat(this.chatId).subscribe(
      (result:Chat)=>{
        console.log(result);
        this.chat = result;
      },
      error =>{
        console.log(error.message);
      }
    )
  }

  async sendMessage() {
    if (!this.inputText) return;

    let messageCreation: MessageCreationDTO = {
      chatId: this.chatId,
      text: this.inputText,
      parentId: null
    };

    console.log(messageCreation);

    await this.connection.invoke('SendMessage', messageCreation);
    this.inputText = '';
  }
}
