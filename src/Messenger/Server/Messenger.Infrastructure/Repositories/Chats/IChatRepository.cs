using Messenger.Domain.Entities;
using Messenger.Infrastructure.Repositories.Base;

namespace Messenger.Infrastructure.Repositories.Chats
{
    public interface IChatRepository : IBaseRepository<Chat, Guid>
    {
    }
}
