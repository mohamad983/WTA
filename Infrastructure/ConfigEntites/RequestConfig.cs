using Domain.Entities.Requests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.ConfigEntites
{
    public class RequestConfig : IEntityTypeConfiguration<Request>
    {
        public void Configure(EntityTypeBuilder<Request> builder)
        {
            builder.ToTable("Requests");
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.Id).ValueGeneratedNever();

            builder.Property(x => x.Title)
              .IsRequired()
              .HasMaxLength(100);

            builder.Property(x => x.RowVersion)
                .IsRowVersion();

            builder.Property(x => x.Description)
              .HasMaxLength(500);

            builder.Ignore(x => x.DomainEvents);

            builder.HasOne(x => x.RequestType)
                .WithMany(x => x.Requests)
                .HasForeignKey(x => x.RequestTypeId);

            builder.HasMany(x => x.RequestValues)
                .WithOne(x => x.Request)
                .HasForeignKey(x => x.RequestId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x=>x.RequestApproval)
                .WithOne(x=>x.Request)
                .HasForeignKey(x=>x.RequestId)
                .OnDelete(DeleteBehavior.Cascade);





        }
    }
}
