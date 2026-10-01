using Domain.Entities.StepFunctions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.ConfigEntites
{
    public class StepFunctionConfig : IEntityTypeConfiguration<StepFunction>
    {
        public void Configure(EntityTypeBuilder<StepFunction> builder)
        {
            builder.ToTable("StepFunction");

            builder.HasKey(x => new
            {
                x.FunctionId,
                x.StepId
            });

            builder.Property(x => x.StepId)
                .IsRequired();

            builder.Property(x => x.FunctionId)
                .IsRequired();

            builder.HasOne(x => x.function)
                .WithMany(m => m.Steps)
                .HasForeignKey(x => x.FunctionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Step)
                .WithMany(s => s.Functions)
                .HasForeignKey(x => x.StepId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
