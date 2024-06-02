export interface Chat {
    id:string,
    link:string|null,
    title:string|null,
    type:ChatType,
    createdAt:string,
    ownerId:number|null,
    membersCount:number,
    loading:boolean
}

export enum ChatType{
    Personal,
    Group,
    Channel
}