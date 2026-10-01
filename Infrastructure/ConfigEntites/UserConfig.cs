using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.ConfigEntites
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
      
            builder.ToTable("Users");

         
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedNever();

          
            builder.Property(x => x.UserName)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(x => x.UserName)
                .IsUnique();

         
            builder.Property(x => x.FullName)
                .IsRequired()
                .HasMaxLength(200);

       
            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(100);

     
            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(100);

     
            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(320);

            builder.HasIndex(x => x.Email)
                .IsUnique();

       
            builder.Property(x => x.PasswordHash)
                .IsRequired()
                .HasMaxLength(500);
           
            builder.Property(x => x.CreatedAt)
                .IsRequired();
            builder.Property(x => x.LastModifiedAt)
                .IsRequired();

            builder.Property(x => x.RowVersion)
                                          .IsRowVersion();
            builder.HasMany(u => u.Roles)
            .WithMany(r => r.Users)
            .UsingEntity<Dictionary<string, object>>(
                "UserRoles",
                j => j.HasOne<Role>().WithMany().HasForeignKey("RoleId").OnDelete(DeleteBehavior.Cascade),
                j => j.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.Cascade),
                j =>
                {
                    j.ToTable("UserRoles");
                    j.HasKey("UserId", "RoleId");
                });

            
            builder.Navigation(u => u.Roles)
                .UsePropertyAccessMode(PropertyAccessMode.Field);


        }
    }
}
