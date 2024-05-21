import { UserViewModel } from "../Users/user-view-model";

export interface MessageDTO {
    id: string;
    text: string;
    createdAt: Date;
    sender?: UserViewModel;
    parent?: MessageDTO;
}
