using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations
{
    public class UserCategoryConfigurations : IEntityTypeConfiguration<UserCategory>
    {
        public void Configure(EntityTypeBuilder<UserCategory> builder)
        {
            builder.HasKey(uc => uc.UserCategoryId);

            builder.HasOne(uc => uc.User)
                   .WithMany(u => u.UserCategories)
                   .HasForeignKey(uc => uc.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(uc => uc.Category)
                   .WithMany(c => c.UserCategories)
                   .HasForeignKey(uc => uc.CategoryId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}