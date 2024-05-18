using Messenger.Domain.Entities;
using Messenger.Infrastructure.Repositories.Base;

namespace Messenger.Infrastructure.Repositories.ChatUsers
{
    public class ChatUserRepository : BaseRepository<ChatUser, Guid>, IChatUserRepository
    {
        public ChatUserRepository(ApplicationDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
