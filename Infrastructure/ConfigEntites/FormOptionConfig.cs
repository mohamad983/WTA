using Domain.Entities.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.ConfigEntites
{
    public class FormOptionConfig : IEntityTypeConfiguration<FormOption>
    {
        public void Configure(EntityTypeBuilder<FormOption> builder)
        {
            builder.ToTable("FormOptions");
            builder.HasKey(x => x.FormOptionId);
            builder.Property(x => x.Value).HasMaxLength(500);
            builder.Property(x => x.Label).HasMaxLength(500);
            builder.Property(x => x.RowVersion)
                                      .IsRowVersion();



        }
    }
}
