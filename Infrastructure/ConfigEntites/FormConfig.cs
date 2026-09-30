using Domain.Entities.Forms;
using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.ConfigEntites
{
    public class FormConfig : IEntityTypeConfiguration<Form>
    {

        public void Configure(EntityTypeBuilder<Form> builder)
        {
            builder.ToTable("Forms");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.Title)
               .IsRequired()
               .HasMaxLength(100);

            builder.Property(x => x.RowVersion)
                                          .IsRowVersion();



            builder.HasMany(x => x.FormInputs)
                 .WithOne(x => x.Form)
                 .HasForeignKey(x => x.FormId)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Cascade);

          
        }
    }
}