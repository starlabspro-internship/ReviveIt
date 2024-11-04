using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations
{
    public class SubscriptionsConfiguration : IEntityTypeConfiguration<Subscriptions>
    {
        public void Configure(EntityTypeBuilder<Subscriptions> builder)
        {
            builder.HasKey(e => e.SubscriptionId);

            builder.Property(e => e.PlanType)
                  .IsRequired()
                  .HasMaxLength(50);

            builder.Property(e => e.Status)
                  .IsRequired()
                  .HasMaxLength(50);

            builder.Property(e => e.StartDate)
                   .IsRequired();

            builder.Property(e => e.EndDate)
                  .IsRequired();

            builder.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("GETDATE()")
                    .ValueGeneratedOnAdd();

            builder.Property(e => e.UpdatedAt)
                  .HasDefaultValueSql("GETDATE()")
                  .ValueGeneratedOnAddOrUpdate();

            builder.HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}