import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Chat } from '../../Interfaces/chat';

@Injectable({
  providedIn: 'root'
})
export class ChatAPIService {

  private apiUrl = 'https://localhost:7031/api/chats';

  constructor(private http: HttpClient) {}

  getUserChats(pageIndex:number = 1){
    return this.http.get<Chat[]>(`${this.apiUrl}/user-active-chats?Page.Index=${pageIndex}`)
  }
}
