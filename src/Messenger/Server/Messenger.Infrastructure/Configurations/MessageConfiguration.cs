using Messenger.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Messenger.Infrastructure.Configurations
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.Property(x => x.Text)
                .HasMaxLength(280);

            builder.HasOne(x => x.Parent)
                .WithMany(x => x.Childrens)
                .HasForeignKey(x => x.ParentId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
