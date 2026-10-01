using Domain.Entities.Requests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.ConfigEntites
{
    public class RequestValueConfig : IEntityTypeConfiguration<RequestValue>
    {
        public void Configure(EntityTypeBuilder<RequestValue> builder)
        {
            builder.ToTable("RequestValues");
            builder.HasKey(x=>x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Ignore(x => x.DomainEvents);
        }
    }
}
