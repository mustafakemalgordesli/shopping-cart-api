using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAPI.Entities;

namespace WebAPI.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Status).IsRequired();
        builder.Property(x => x.Email).IsRequired().HasMaxLength(100);
        builder.Property(x => x.FirstName).IsRequired().HasMaxLength(100);
        builder.Property(x => x.CreatedDate).IsRequired();
        builder.Property(x => x.PasswordHash).IsRequired().HasMaxLength(500);
        builder.Property(x => x.PasswordSalt).IsRequired().HasMaxLength(500);
        builder.Property(x => x.LastName).HasMaxLength(100);
        builder.Property(x => x.Role).IsRequired().HasMaxLength(50);
    }
}
