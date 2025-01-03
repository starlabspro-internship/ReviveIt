﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Domain.Configurations
{
    public class ChatSessionConfigurations : IEntityTypeConfiguration<ChatSession>
    {
        public void Configure(EntityTypeBuilder<ChatSession> builder)
        {
            builder.HasKey(cs => cs.ChatSessionId);

            builder.Property(cs => cs.StartTime)
                   .HasDefaultValueSql("GETDATE()");

            builder.HasOne(cs => cs.Technician)
                   .WithMany(u => u.TechnicianChatSessions)
                   .HasForeignKey(cs => cs.TechnicianId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(cs => cs.Customer)
                   .WithMany(u => u.CustomerChatSessions)
                   .HasForeignKey(cs => cs.CustomerId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(cs => cs.Company)
                   .WithMany(u => u.CompanyChatSessions)
                   .HasForeignKey(cs => cs.CompanyId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

