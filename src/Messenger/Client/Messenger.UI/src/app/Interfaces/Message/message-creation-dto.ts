export interface MessageCreationDTO {
    parentId:string|null,
    chatId:string,
    mediaFile?: File | null;
    text:string
}
