using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations
{
    public class CategoryConfigurations : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.CategoryID);

            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);

            builder.Property(c => c.CreatedAt).HasDefaultValueSql("GETDATE()");
            builder.Property(c => c.UpdatedAt).HasDefaultValueSql("GETDATE()");

            builder.HasMany(c => c.Jobs)
                   .WithOne(j => j.Category)
                   .HasForeignKey(j => j.CategoryId)
                   .OnDelete(DeleteBehavior.SetNull);  
        }
    }
}