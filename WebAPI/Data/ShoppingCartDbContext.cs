using Microsoft.EntityFrameworkCore;
using WebAPI.Data.Configurations;
using WebAPI.Entities;

namespace WebAPI.Data;

public class ShoppingCartDbContext : DbContext 
{
    public ShoppingCartDbContext(DbContextOptions<ShoppingCartDbContext> options) : base(options)
    {

    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        builder.Entity<Product>()
        .Property(p => p.Price)
        .HasColumnType("decimal(18,2)");
        builder
            .ApplyConfiguration(new ProductConfiguration());

        builder
            .ApplyConfiguration(new CategoryConfiguration());
        builder
            .ApplyConfiguration(new CartItemConfiguration());
        builder
            .ApplyConfiguration(new CartConfiguration());
        builder
            .ApplyConfiguration(new UserConfiguration());
    }


    public override int SaveChanges()
    {
        OnBeforeSave();
        return base.SaveChanges();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        OnBeforeSave();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        OnBeforeSave();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        OnBeforeSave();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void OnBeforeSave()
    {
        var addedEntities = ChangeTracker.Entries()
            .Where(i => i.State == EntityState.Added)
            .Select(i => (BaseEntity)i.Entity);
        PrepareAddedEntities(addedEntities);
    }

    private void PrepareAddedEntities(IEnumerable<BaseEntity> entities)
    {
        foreach (var entity in entities)
        {
            if (entity.CreatedDate == DateTime.MinValue)
                entity.CreatedDate = DateTime.Now;
        }
    }
}
