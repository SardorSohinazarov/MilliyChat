namespace Messenger.Domain.Common
{
    public class Auditable<TId>
    {
        public TId Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
