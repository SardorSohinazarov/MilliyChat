import { Injectable } from '@angular/core';
import { environment } from '../../../environment/environment';
import { HttpClient } from '@angular/common/http';
import { Chat } from '../../Models/Chats/chat';
import { GroupChatCreationDTO } from '../../Models/Chats/group-chat-creation-dto';
import { ChannelChatCreationDTO } from '../../Models/Chats/channel-chat-creation-dto';

@Injectable({
  providedIn: 'root'
})
export class ChatService {

  private apiUrl = `${environment.apiUrl}api/chats`;

  constructor(private http: HttpClient) {}

  getChat(chatId:string){
    return this.http.get<Chat>(`${this.apiUrl}/${chatId}`)
  }

  getUserChats(pageIndex:number = 1){
    return this.http.get<Chat[]>(`${this.apiUrl}/user-active-chats?Page.Index=${pageIndex}&Page.Size=2`)
  }

  createChannel(channelChatCreationDTO:ChannelChatCreationDTO){
    return this.http.post<string>(`${this.apiUrl}/create-channel`,channelChatCreationDTO);
  }

  createGroup(groupChatCreationDTO:GroupChatCreationDTO){
    return this.http.post<string>(`${this.apiUrl}/create-group`,groupChatCreationDTO);
  }
}
