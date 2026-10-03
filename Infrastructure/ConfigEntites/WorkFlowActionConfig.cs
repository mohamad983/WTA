using Domain.Entities.RequestTypes;
using Domain.Entities.WorkFlowActions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.ConfigEntites
{
    public class WorkFlowActionConfig : IEntityTypeConfiguration<WorkFlowAction>
    {
        public void Configure(EntityTypeBuilder<WorkFlowAction> builder)
        {
            builder.ToTable("WorkFlowActions");
            
            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.IsSystem)
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(150);

            builder.Ignore(x => x.DomainEvents);

            builder.Property(x => x.RowVersion)
                .IsRowVersion();

            builder.HasOne<RequestType>()
                .WithMany(rt => rt.Actions)
                .HasForeignKey(x => x.RequestTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new { x.RequestTypeId, x.Code })
                .IsUnique()
                .HasFilter("[IsDeleted] = 0");
        }
    }
}
