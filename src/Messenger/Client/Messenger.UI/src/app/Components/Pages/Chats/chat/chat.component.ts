import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import { MessageAPIService } from '../../../../Services/MessagesAPIServices/message-api.service';
import { MessageDTO } from '../../../../Interfaces/Message/message-dto';
import { MessageCreationDTO } from '../../../../Interfaces/Message/message-creation-dto';
import { AuthService } from '../../../../Services/AuthServices/auth.service';
import { TextMessageComponent } from '../../../Messages/text-message/text-message.component';
import { UserViewModel } from '../../../../Interfaces/Users/user-view-model';
import { Chat } from '../../../../Interfaces/Chat/chat';
import { ChatAPIService } from '../../../../Services/ChatAPIServices/chat-api.service';
import { error } from 'console';
import { NavbarComponent } from '../../../Navbars/navbar/navbar.component';
import { ChatNavbarComponent } from '../../../Navbars/chat-navbar/chat-navbar.component';

@Component({
  selector: 'app-chat',
  standalone: true,
  imports: [
    FormsModule,
    TextMessageComponent,
    ChatNavbarComponent
  ],
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent implements OnInit {

  private connection: HubConnection;
  public messages: MessageDTO[] = [];
  public inputText: string = '';
  private chatId: string = '';
  public userInfo: UserViewModel | null = null;
  public chat!:Chat;

  constructor(
    private activatedRoute: ActivatedRoute,
    private messageAPIService: MessageAPIService,
    private chatAPIService:ChatAPIService,
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

  async ngOnInit() {
    this.chatId = this.activatedRoute.snapshot.params['chatId'];
    console.log('chatId:' + this.chatId);

    this.loadUser();

    this.getChat();
    this.getMessages();

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