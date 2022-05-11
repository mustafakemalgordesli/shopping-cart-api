using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAPI.Entities;

namespace WebAPI.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.Description).IsRequired().HasMaxLength(255);
            builder.Property(x => x.CategoryId).IsRequired();
            builder.Property(x => x.ImageURL).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.CreatedDate).IsRequired();

            builder.HasOne(x => x.Category).WithMany(x => x.Products).HasForeignKey(x => x.CategoryId);
        }
    }
}
