import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Chat } from '../../Interfaces/Chat/chat';
import { get } from 'http';
import { ChannelChatCreationDTO } from '../../Interfaces/Chat/channel-chat-creation-dto';
import { GroupChatCreationDTO } from '../../Interfaces/Chat/group-chat-creation-dto';

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

  createChannel(channelChatCreationDTO:ChannelChatCreationDTO){
    return this.http.post<string>(`${this.apiUrl}/create-channel`,channelChatCreationDTO);
  }

  createGroup(groupChatCreationDTO:GroupChatCreationDTO){
    return this.http.post<string>(`${this.apiUrl}/create-group`,groupChatCreationDTO);
  }
}
