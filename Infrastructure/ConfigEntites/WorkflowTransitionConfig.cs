using Domain.Entities.WorkFlowStepTransitions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.ConfigEntites
{
    public class WorkflowTransitionConfig : IEntityTypeConfiguration<WorkFlowStepTransition>
    {
        public void Configure(EntityTypeBuilder<WorkFlowStepTransition> builder)
        {
            builder.ToTable("WorkflowStepTransitions");

            builder.HasKey(x => x.WorkFlowStepTransitionId);

            builder.Ignore(x => x.DomainEvents);

            builder.Property(x => x.CurrentStepId)
                .IsRequired();
            
            builder.Property(x => x.IsDeleted)
                .IsRequired();

            builder.Property(x => x.NextStepId)
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.Property(x => x.RowVersion)
                .IsRowVersion();

            builder.HasOne(x => x.CurrentStep)
                .WithMany(st => st.transitions)
                .HasForeignKey(x => x.CurrentStepId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.NextStep)
                .WithMany()
                .HasForeignKey(x => x.NextStepId)
                .OnDelete(DeleteBehavior.Restrict);
            //builder.Property(x => x.)
        }
    }
}
