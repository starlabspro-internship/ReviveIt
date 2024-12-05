using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations
{
    public class UsersConfigurations : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.HasKey(u => u.Id);
           
            builder.Property(u => u.Role).IsRequired().HasMaxLength(50);

            builder.Property(u => u.CreatedAt).HasDefaultValueSql("GETDATE()");

            builder.Property(u => u.UpdatedAt).HasDefaultValueSql("GETDATE()");

        }
    }
}