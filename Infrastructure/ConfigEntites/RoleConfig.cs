using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Infrastructure.ConfigEntites
{
    public class RoleConfig : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.RoleImportance)
                .IsRequired();

            builder.Property(x => x.RoleName)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(x => x.RoleName)
                .IsUnique()
                .HasFilter("[IsDeleted] = 0");

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.Property(x => x.RowVersion)
                .IsRowVersion();

            builder.Ignore(x => x.DomainEvents);

            builder.Property(x => x.IsDeleted)
                .IsRequired();

            builder.Property(x => x.IsSystem)
                .IsRequired();

            builder.HasMany(x => x.Permissions)
                .WithOne(pr => pr.Role)
                .HasForeignKey(x => x.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Navigation(x => x.Permissions)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
