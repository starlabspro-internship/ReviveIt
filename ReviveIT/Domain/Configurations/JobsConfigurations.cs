using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations
{
    public class JobsConfigurations : IEntityTypeConfiguration<Jobs>
    {
        public void Configure(EntityTypeBuilder<Jobs> builder)
        {
            builder.HasKey(c => c.JobID);

            builder.Property(e => e.Title).IsRequired().HasMaxLength(100);

            builder.Property(e => e.Description).IsRequired().HasMaxLength(1000);

            builder.Property(e => e.Category).IsRequired().HasMaxLength(50);

            builder.Property(e => e.Status).IsRequired().HasMaxLength(20);

            builder.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");

            builder.Property(e => e.UpdatedAt).HasDefaultValueSql("GETDATE()");

            builder.HasOne(e => e.User)
            .WithMany()
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(e => e.Company)
           .WithMany()
           .HasForeignKey(e => e.CompanyId)
           .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
