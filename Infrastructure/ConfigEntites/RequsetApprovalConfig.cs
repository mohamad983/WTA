using Domain.Entities.Requests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.ConfigEntites
{
    public class RequsetApprovalConfig : IEntityTypeConfiguration<RequestApproval>
    {
        public void Configure(EntityTypeBuilder<RequestApproval> builder)
        {
            builder.ToTable("RequestApprovals");
            builder.HasKey(x=>x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Ignore(x => x.DomainEvents);
        }
    }
}
