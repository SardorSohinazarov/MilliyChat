using Microsoft.AspNetCore.Http;

namespace Messenger.Application.DataTransferObjects.Messages
{
    public class MessageCreationDTO
    {
        public long SenderId { get; set; }
        public Guid? ParentId { get; set; }
        public Guid ChatId { get; set; }
        public IFormFile? MediaFile { get; set; }
        public string Text { get; set; }
    }
}
