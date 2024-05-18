import { Component, NgModule, OnInit } from '@angular/core';
import { FormsModule, NgModel, ReactiveFormsModule } from '@angular/forms';
import { HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';

@Component({
  selector: 'app-chat',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.scss'
})
export class ChatComponent implements OnInit{

  private connection: HubConnection;
  public messages:string[] = [];
  public user: string = "";
  public message: string = "";

  constructor(){
    this.connection = new HubConnectionBuilder()
      .withUrl('https://localhost:7031/chathub')
      .configureLogging(LogLevel.Information)
      .build();
  }

  async ngOnInit() {
    this.connection.on('ReceiveMessage', (user,message) => {
      this.messages.push(user+':'+message);
    });

    try {
      await this.connection.start();
      console.log('Connected to SignalR hub');
    } catch (error) {
      console.error('Failed to connect to SignalR hub', error);
    }
  }

  async sendMessage() {
    if (!this.user || !this.message) return;
    await this.connection.invoke('SendMessage', this.user, this.message);
    this.message = '';
  }

}
