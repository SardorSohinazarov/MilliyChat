import { Chat } from "../chat";

export interface UserProfileViewModel {
    id:number,
    firstName:string,
    phoneNumber:string,
    lastName:string|null,
    userName:string|null,
    email:string|null,
    photoPath:string|null,
    authorshipChats:Chat[],
    createdAt:Date
}
