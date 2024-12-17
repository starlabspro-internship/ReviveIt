using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Domain.Configurations
{
    public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
    {
        public void Configure(EntityTypeBuilder<Feedback> builder)
        {
            builder.HasKey(f => f.Id);
            builder.Property(f => f.Message).IsRequired().HasMaxLength(500);
            builder.Property(f => f.SurveyResponse).HasDefaultValue(null);
            builder.Property(f => f.FeatureSuggestion).HasMaxLength(500).HasDefaultValue(null);
            builder.Property(f => f.Date).IsRequired();
            builder.Property(f => f.UserId).IsRequired(false); 

            builder.HasOne(f => f.User)
                   .WithMany()
                   .HasForeignKey(f => f.UserId)
                   .OnDelete(DeleteBehavior.SetNull); 
        }
    }
}