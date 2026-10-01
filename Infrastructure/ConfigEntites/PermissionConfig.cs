using Domain.Entities.Permissions;
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
            builder.ToTable("Permission");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.IsDeleted)
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.Property(x => x.DisplayName)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(x => x.Key)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasIndex(x => x.Key)
                .IsUnique();

            builder.Property(x => x.Kind).IsRequired().HasConversion<string>().HasMaxLength(30);

            builder.Property(x => x.GroupName)
                .HasMaxLength(300);

            builder.Property(x => x.RowVersion)
                .IsRowVersion();
        }
    }
}
