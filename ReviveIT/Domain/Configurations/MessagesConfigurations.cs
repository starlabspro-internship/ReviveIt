using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Domain.Configurations
{
    public class MessagesConfigurations : IEntityTypeConfiguration<Messages>
    {
        public void Configure(EntityTypeBuilder<Messages> builder)
        {
            builder.HasKey(m => m.MessageID);

            builder.Property(m => m.MessageContent)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(m => m.Timestamp)
                   .HasDefaultValueSql("GETDATE()");

            builder.Property(m => m.Viewed)
                   .HasDefaultValue(false);

            builder.HasOne(m => m.Sender)
                   .WithMany()
                   .HasForeignKey(m => m.SenderID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.Recipient)
                   .WithMany()
                   .HasForeignKey(m => m.RecipientID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.ChatSession)
                   .WithMany(cs => cs.Messages)
                   .HasForeignKey(m => m.ChatSessionId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
