using Messenger.Domain.Entities;
using Messenger.Infrastructure.Repositories.Base;

namespace Messenger.Infrastructure.Repositories.Chats
{
    public class ChatRepository : BaseRepository<Chat, Guid>, IChatRepository
    {
        public ChatRepository(ApplicationDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
