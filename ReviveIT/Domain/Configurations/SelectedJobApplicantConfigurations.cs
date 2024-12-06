using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations
{
    public class SelectedJobApplicantConfigurations : IEntityTypeConfiguration<SelectedJobApplicant>
    {
        public void Configure(EntityTypeBuilder<SelectedJobApplicant> builder)
        {
            builder.HasKey(sja => sja.SelectedApplicantID);

            builder.HasOne(sja => sja.Job)
                   .WithMany(j => j.SelectedJobApplicants)
                   .HasForeignKey(sja => sja.JobID)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(sja => sja.JobApplication)
                   .WithMany()
                   .HasForeignKey(sja => sja.ApplicationID)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(sja => sja.SelectedApplicantUser)
                   .WithMany()
                   .HasForeignKey(sja => sja.SelectedApplicantUserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(sja => sja.SelectedByUser)
                   .WithMany()
                   .HasForeignKey(sja => sja.SelectedByUserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(sja => sja.SelectedDate)
                   .IsRequired();
        }
    }
}