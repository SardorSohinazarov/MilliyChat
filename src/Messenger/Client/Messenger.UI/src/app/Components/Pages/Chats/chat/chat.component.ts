import { Component, NgModule, OnInit } from '@angular/core';
import { FormsModule, NgModel, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import { MessageAPIService } from '../../../../Services/MessagesAPIServices/message-api.service';
import { MessageDTO } from '../../../../Interfaces/Message/message-dto';
import { UserInfo } from 'os';
import { MessageCreationDTO } from '../../../../Interfaces/Message/message-creation-dto';
import { AuthService } from '../../../../Services/AuthServices/auth.service';
import { TextMessageComponent } from '../../../Messages/text-message/text-message.component';
import { UserViewModel } from '../../../../Interfaces/Users/user-view-model';

@Component({
  selector: 'app-chat',
  standalone: true,
  imports: [
    FormsModule,
    TextMessageComponent
  ],
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.scss'
})
export class ChatComponent implements OnInit{

  private connection: HubConnection;
  public messages:MessageDTO[] = [];
  public inputText: string = '';
  private chatId:string = '';
  public userInfo:UserViewModel|null = null;

  constructor(
    private activatedRoute:ActivatedRoute,
    private messageAPIService:MessageAPIService,
    private authService:AuthService
  ){
    this.connection = new HubConnectionBuilder()
      .withUrl('https://localhost:7031/chathub',
      {
        accessTokenFactory:() => this.authService.getAccessToken()!
      })
      .configureLogging(LogLevel.Information)
      .build();
  }

  async ngOnInit() {
    this.chatId = this.activatedRoute.snapshot.params['chatId'];
    console.log('chatId:' + this.chatId);

    this.loadUser();
    console.log(this.userInfo)

    this.messageAPIService.getChatMessages(this.chatId).subscribe(
      (messages:MessageDTO[]) =>{
        console.log(messages)
        this.messages = messages
      }
    )

    this.connection.on('ReceiveMessage', (message) => {
      console.log("message keldi:"+message);
      this.messages.push(message);
    });

    try {
      await this.connection.start();
      console.log('Connected to SignalR hub');
    } catch (error) {
      console.error('Failed to connect to SignalR hub', error);
    }
  }

  loadUser(): void {
    const serializedData = localStorage.getItem('userProfile');
    if (serializedData) {
      this.userInfo = JSON.parse(serializedData) as UserViewModel;
      console.log(this.userInfo)
    }
  }

  async sendMessage() {
    if (!this.inputText) return;
    
    let messageCreation:MessageCreationDTO ={
      chatId:this.chatId,
      text:this.inputText,
      parentId:null
    }

    console.log(messageCreation)

    await this.connection.invoke('SendMessage',messageCreation);
    this.inputText = '';
  }
}
