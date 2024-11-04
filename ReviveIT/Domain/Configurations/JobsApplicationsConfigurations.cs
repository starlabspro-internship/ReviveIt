using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations
{
    public class JobsApplicationsConfigurations : IEntityTypeConfiguration<JobApplication>
    {
        public void Configure(EntityTypeBuilder<JobApplication> builder)
        {
            builder.HasKey(e => e.ApplicationID);

            builder.Property(e => e.ApplicationDate).IsRequired();

            builder.Property(e => e.Status).IsRequired().HasMaxLength(20);

            builder.HasOne(e => e.Job)
                  .WithMany(j => j.JobApplications)
                  .HasForeignKey(e => e.JobID)
                  .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.User)
                  .WithMany()
                  .HasForeignKey(e => e.UserId)
                  .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
