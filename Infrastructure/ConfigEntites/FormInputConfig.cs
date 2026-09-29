using Domain.Entities.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.ConfigEntites
{
    public class FormInputConfig : IEntityTypeConfiguration<FormInput>
    {
        public void Configure(EntityTypeBuilder<FormInput> builder)
        {
            builder.ToTable("FormInputs");
            builder.HasKey(x => x.FormInputId);
            builder.Property(x => x.FormInputId).ValueGeneratedNever();
            builder.Property(x => x.Label).HasMaxLength(500);
            builder.Property(x => x.RowVersion)
                                      .IsRowVersion();

            builder.HasMany(x => x.FormOptions)
                  .WithOne(x => x.FormInput)
                  .HasForeignKey(x => x.FormInputId)
                  .IsRequired()
                  .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
