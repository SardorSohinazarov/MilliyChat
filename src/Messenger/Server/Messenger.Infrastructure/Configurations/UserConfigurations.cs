using Messenger.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Messenger.Infrastructure.Configurations
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(x => x.PhoneNumber)
                .IsUnique();

            builder.HasIndex(x => x.Username)
                .IsUnique();

            builder.HasMany(x => x.Chats)
                .WithMany(x => x.Users)
                .UsingEntity(nameof(ChatUser));
        }
    }
}
