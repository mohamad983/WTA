using Domain.Entities.Functions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.ConfigEntites
{
    public class FunctionConfig : IEntityTypeConfiguration<Function>
    {
        public void Configure(EntityTypeBuilder<Function> builder)
        {
            builder.ToTable("Functions");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.Property(x => x.IsDeleted)
                .IsRequired();

            builder.Property(x => x.RowVersion)
                .IsRowVersion();

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Script)
                .HasMaxLength(5000);

            builder.Ignore(x => x.DomainEvents);

            builder.HasMany(x => x.Steps)
                .WithOne(m => m.function)
                .HasForeignKey(m => m.FunctionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Transitions)
                .WithOne(m => m.function)
                .HasForeignKey(m => m.FunctionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Navigation(x => x.Steps)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Navigation(x => x.Transitions)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
