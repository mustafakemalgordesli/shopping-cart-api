using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAPI.Entities;

namespace WebAPI.Data.Configurations;

public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CreatedDate).IsRequired();
        builder.Property(x => x.Quantity).IsRequired();
        builder.Property(x => x.ProductId).IsRequired();
        builder.Property(x => x.CartId).IsRequired();
        builder.HasOne(x => x.Cart).WithMany(x => x.Items).HasForeignKey(x => x.CartId);
    }
}
