using Messenger.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Messenger.Infrastructure.Configurations
{
    public class ChatConfiguration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.HasMany(x => x.Users)
                .WithOne(x => x.Chat)
                .HasForeignKey(x => x.ChatId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
