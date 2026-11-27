using Microsoft.EntityFrameworkCore;
using RestaurantManagement.OrderService.Entities;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace RestaurantManagement.OrderService.EntityFrameworkCore;

[ConnectionStringName("Default")]
public class OrderServiceDbContext : AbpDbContext<OrderServiceDbContext>
{
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    public OrderServiceDbContext(DbContextOptions<OrderServiceDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Order>(b =>
        {
            b.ToTable("Orders");
            b.ConfigureByConvention();
            b.Property(x => x.OrderCode).IsRequired().HasMaxLength(32);
            b.HasIndex(x => x.OrderCode).IsUnique();
            b.Property(x => x.SubTotal).HasPrecision(18, 2);
            b.Property(x => x.DiscountAmount).HasPrecision(18, 2);
            b.Property(x => x.VatAmount).HasPrecision(18, 2);
            b.Property(x => x.TotalAmount).HasPrecision(18, 2);
            b.Property(x => x.Note).HasMaxLength(500);
            b.HasMany(x => x.Items).WithOne().HasForeignKey(x => x.OrderId).OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<OrderItem>(b =>
        {
            b.ToTable("OrderItems");
            b.ConfigureByConvention();
            b.Property(x => x.MenuItemName).IsRequired().HasMaxLength(200);
            b.Property(x => x.UnitPrice).HasPrecision(18, 2);
            b.Property(x => x.LineTotal).HasPrecision(18, 2);
            b.Property(x => x.Note).HasMaxLength(300);
        });
    }
}
