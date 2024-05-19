namespace Messenger.Application.DataTransferObjects.ChatUsers
{
    public class ChatUserModificationDTO
    {
        public bool IsBlocked { get; set; }
        public Guid ChatId { get; set; }
        public long UserId { get; set; }
    }
}
