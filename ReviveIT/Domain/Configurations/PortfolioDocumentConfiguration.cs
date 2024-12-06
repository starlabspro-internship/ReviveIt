using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations
{
    public class PortfolioDocumentConfiguration : IEntityTypeConfiguration<PortfolioDocument>
    {
        public void Configure(EntityTypeBuilder<PortfolioDocument> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Title)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(p => p.FilePath)
                   .HasMaxLength(200)
                   .IsRequired(); 

            builder.Property(p => p.FileType)
                   .HasMaxLength(50);

            builder.Property(p => p.UploadedAt)
                   .HasDefaultValueSql("GETDATE()"); 

            builder.HasOne(p => p.User)
                   .WithMany(u => u.Portfolios) 
                   .HasForeignKey(p => p.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}