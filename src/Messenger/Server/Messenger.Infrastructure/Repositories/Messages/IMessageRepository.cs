using Messenger.Domain.Entities;
using Messenger.Infrastructure.Repositories.Base;

namespace Messenger.Infrastructure.Repositories.Messages
{
    public interface IMessageRepository : IBaseRepository<Message, Guid>
    {
    }
}
