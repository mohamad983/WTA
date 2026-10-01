using Domain.Entities.TransitionFunctions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.ConfigEntites
{
    public class TransitionFunctionConfig : IEntityTypeConfiguration<TransitionFunction>
    {
        public void Configure(EntityTypeBuilder<TransitionFunction> builder)
        {
            builder.ToTable("TransitionFunction");

            builder.HasKey(x => new
            {
                x.FunctionId,
                x.TransitionId
            });

            builder.Property(x => x.TransitionId)
                .IsRequired();

            builder.Property(x => x.FunctionId)
                .IsRequired();

            builder.HasOne(x => x.Transition)
                .WithMany(t => t.Functions)
                .HasForeignKey(x => x.TransitionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.function)
                .WithMany(f => f.Transitions)
                .HasForeignKey(x => x.FunctionId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
