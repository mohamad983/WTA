using Domain.Entities.WorkFlowSteps;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.ConfigEntites
{
    public class WorkflowStepConfig : IEntityTypeConfiguration<WorkFlowStep>
    {
        public void Configure(EntityTypeBuilder<WorkFlowStep> builder)
        {
            builder.ToTable("WorkFlowSteps");

            builder.HasKey(x => x.WorkFlowStepId);
            
            builder.Property(wfs => wfs.ApproverRoleId)
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .IsRequired();
            
            builder.Property(x => x.IsDeleted)
                .IsRequired();

            builder.Property(x => x.RequestTypeId)
                .IsRequired();

            builder.Property(x => x.RowVersion)
                .IsRowVersion();

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.StepOrder)
                .IsRequired();

            builder.Property(x => x.LastModifiedAt)
                .IsRequired();
            
            builder.Ignore(x => x.DomainEvents);

            builder.HasMany(x => x.transitions)
                .WithOne(y => y.CurrentStep)
                .HasForeignKey(y => y.CurrentStepId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ApproverRole)
                .WithMany()
                .HasForeignKey(x => x.ApproverRoleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.ApproverUser)
                .WithMany()
                .HasForeignKey(x => x.ApproverUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Functions)
                .WithOne(m => m.Step)
                .HasForeignKey(m => m.StepId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.Navigation(x => x.transitions)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
            
            builder.Navigation(x => x.Functions)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
