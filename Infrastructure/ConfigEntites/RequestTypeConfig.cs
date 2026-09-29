using Domain.Entities.RequestTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.ConfigEntites
{
    public class RequestTypeConfig : IEntityTypeConfiguration<RequestType>
    {
        public void Configure(EntityTypeBuilder<RequestType> builder)
        {
            builder.ToTable("RequestTypes");
            
            builder.HasKey(rt => rt.RequestTypeGuid);
            
            builder.Property(rt => rt.CreatedAt)
                .IsRequired();
            
            builder.Property(rt => rt.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(rt => rt.Code)
                .IsRequired();
            builder.HasIndex(rt => rt.Code)
                .IsUnique();

            builder.Property(rt => rt.Title)
                .IsRequired()
                .HasMaxLength(500);
            builder.HasIndex(rt => rt.Title)
                .IsUnique();

            builder.Property(rt => rt.IsDeleted)
                .IsRequired();

            /*builder.Property(rt => rt.LastModifiedAt)
                .IsRequired();*/

            builder.Property(rt => rt.RowVersion)
                .IsRowVersion();

            builder.HasMany(rt => rt.Steps)
                .WithOne(wfs => wfs.RequestType)
                .HasForeignKey(wfs => wfs.RequestTypeId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Navigation(x => x.Steps)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
