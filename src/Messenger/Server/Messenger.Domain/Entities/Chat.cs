using Messenger.Domain.Common;
using Messenger.Domain.Enums;

namespace Messenger.Domain.Entities
{
    public class Chat : Auditable<Guid>
    {
        public string? Link { get; set; }
        public string? Title { get; set; }
        public ChatType Type { get; set; }
        public long? OwnerId { get; set; }

        public virtual User Owner { get; set; }
        public virtual IEnumerable<ChatUser> Users { get; set; }
        public virtual List<Message> Messages { get; set; }
    }
}
