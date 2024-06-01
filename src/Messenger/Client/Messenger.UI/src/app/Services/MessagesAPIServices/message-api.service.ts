import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MessageDTO } from '../../Interfaces/Message/message-dto';
import { MessageCreationDTO } from '../../Interfaces/Message/message-creation-dto';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MessageAPIService {
  private apiUrl = `${environment.apiUrl}api/messages`;

  constructor(private http: HttpClient) {}

  getChatMessages(chatId:string,pageIndex:number = 1){
    return this.http.get<MessageDTO[]>(`${this.apiUrl}/chat/${chatId}?Page.Index=${pageIndex}`)
  }

  deleteMessage(messageId:string){
    return this.http.delete(`${this.apiUrl}/${messageId}`);
  }

  createMessage(chatId:string,messageCreationDTO:MessageCreationDTO){
    return this.http.post<MessageDTO>(`${this.apiUrl}`,messageCreationDTO);
  }
}
