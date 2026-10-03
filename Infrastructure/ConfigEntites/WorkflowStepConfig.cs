using Domain.Entities.RequestTypes;
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

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.Property(x => x.CreatedAt)
                .IsRequired();
            
            builder.Property(x => x.IsDeleted)
                .IsRequired();

            builder.Property(x => x.RowVersion)
                .IsRowVersion();

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.StepOrder)
                .IsRequired();

            builder.Property(x => x.Kind)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(20);

            builder.HasIndex(x => new { x.RequestTypeId, x.StepOrder});
            
            builder.Ignore(x => x.DomainEvents);

            builder.HasOne<RequestType>(x => x.RequestType)
                .WithMany(rt => rt.Steps)
                .HasForeignKey(x => x.RequestTypeId)
                .OnDelete(DeleteBehavior.Cascade);

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
