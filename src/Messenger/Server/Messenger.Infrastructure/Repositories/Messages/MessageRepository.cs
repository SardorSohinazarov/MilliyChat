using Messenger.Domain.Entities;
using Messenger.Infrastructure.Repositories.Base;

namespace Messenger.Infrastructure.Repositories.Messages
{
    public class MessageRepository : BaseRepository<Message, Guid>, IMessageRepository
    {
        public MessageRepository(ApplicationDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
