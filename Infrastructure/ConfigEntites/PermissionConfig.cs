using Domain.Entities.Permissions;
using Domain.Entities.RequestTypes;
using Domain.Entities.WorkFlowActions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.ConfigEntites
{
    public class PermissionConfig : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permissions");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Ignore(x => x.DomainEvents);

            builder.Property(x => x.Key).IsRequired().HasMaxLength(200);
            builder.HasIndex(x => x.Key).IsUnique();     // no filter: the sync restores deleted ones

            builder.Property(x => x.DisplayName).IsRequired().HasMaxLength(300);
            builder.Property(x => x.GroupName).HasMaxLength(300);
            builder.Property(x => x.Kind).IsRequired().HasConversion<string>().HasMaxLength(30);

            builder.Property(x => x.ActionCode).HasMaxLength(50);
            builder.Property(x => x.ActionTitle).HasMaxLength(150);

            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.RowVersion).IsRowVersion();

            builder.HasIndex(x => x.RequestTypeId);
            builder.HasIndex(x => x.WorkFlowActionId);

            // Other aggregates are referenced by ID only; no navigation properties.
            builder.HasOne<RequestType>()
                .WithMany()
                .HasForeignKey(x => x.RequestTypeId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<WorkFlowAction>()
                .WithMany()
                .HasForeignKey(x => x.WorkFlowActionId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
