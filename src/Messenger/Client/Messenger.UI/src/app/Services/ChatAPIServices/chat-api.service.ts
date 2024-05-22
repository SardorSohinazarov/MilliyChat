import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Chat } from '../../Interfaces/chat';
import { get } from 'http';

@Injectable({
  providedIn: 'root'
})
export class ChatAPIService {

  private apiUrl = 'https://localhost:7031/api/chats';

  constructor(private http: HttpClient) {}

  getChat(chatId:string){
    return this.http.get<Chat>(`${this.apiUrl}/${chatId}`)
  }

  getUserChats(pageIndex:number = 1){
    return this.http.get<Chat[]>(`${this.apiUrl}/user-active-chats?Page.Index=${pageIndex}`)
  }

  getOrCreateJoinChat(userId:number){
    return this.http.get<string>(`${this.apiUrl}/get-create-join/${userId}`);
  }
}
