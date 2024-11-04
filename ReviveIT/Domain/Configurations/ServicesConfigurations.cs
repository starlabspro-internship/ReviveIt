using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations
{
    public class ServicesConfigurations : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.HasKey(c => c.ServiceID);

            builder.Property(e => e.ServiceName).IsRequired();

            builder.Property(e => e.Category).IsRequired();

            builder.Property(e => e.Description).IsRequired();

            builder.Property(e => e.Price).IsRequired();

            builder.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");

            builder.Property(e => e.UpdatedAt).HasDefaultValueSql("GETDATE()");

            builder.HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
