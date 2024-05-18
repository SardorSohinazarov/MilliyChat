using Messenger.Domain.Common;
using Messenger.Domain.Enums;

namespace Messenger.Domain.Entities
{
    public class Chat : Auditable<Guid>
    {
        public string Link { get; set; }
        public string? Title { get; set; }
        public ChatType Type { get; set; }

        public virtual List<User> Users { get; set; }
        public virtual List<Message> Messages { get; set; }
    }
}
