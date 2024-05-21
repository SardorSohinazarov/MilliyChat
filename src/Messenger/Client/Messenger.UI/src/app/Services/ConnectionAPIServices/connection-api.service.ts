import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ConnectionAPIService {

  private apiUrl = 'https://localhost:7031/api/connections/join-chat/764c8ecc-a0aa-4a55-1b39-08dc788d9442';

  constructor(private http: HttpClient) {}

  joinChat(chatId:string,pageIndex:number = 1){
    return this.http.get<string>(`${this.apiUrl}/chat/${chatId}?Page.Index=${pageIndex}`)
  }

  addMember(messageId:string){
    return this.http.delete(`${this.apiUrl}/${messageId}`);
  }

  kickMember(chatId:string){
    return this.http.get(`${this.apiUrl}`);
  }

  blockChatMember(chatId:string){
    return this.http.get(`${this.apiUrl}`);
  }

  blockChat(chatId:string){
    return this.http.get(`${this.apiUrl}`);
  }

  leaveChat(chatId:string){
    return this.http.get(`${this.apiUrl}`);
  }
}
