using Microsoft.EntityFrameworkCore;
using RestaurantManagement.MenuService.Entities;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace RestaurantManagement.MenuService.EntityFrameworkCore;

[ConnectionStringName("Default")]
public class MenuServiceDbContext : AbpDbContext<MenuServiceDbContext>
{
    public DbSet<MenuCategory> MenuCategories => Set<MenuCategory>();
    public DbSet<MenuItem> MenuItems => Set<MenuItem>();
    public DbSet<BranchMenuItem> BranchMenuItems => Set<BranchMenuItem>();
    public DbSet<MenuPrintTemplate> MenuPrintTemplates => Set<MenuPrintTemplate>();

    public MenuServiceDbContext(DbContextOptions<MenuServiceDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<MenuCategory>(b =>
        {
            b.ToTable("MenuCategories");
            b.ConfigureByConvention();
            b.Property(x => x.NameVi).IsRequired().HasMaxLength(150);
            b.Property(x => x.NameKo).HasMaxLength(150);
            b.Property(x => x.NameEn).HasMaxLength(150);
        });

        builder.Entity<MenuItem>(b =>
        {
            b.ToTable("MenuItems");
            b.ConfigureByConvention();
            b.Property(x => x.Code).IsRequired().HasMaxLength(32);
            b.HasIndex(x => x.Code).IsUnique();
            b.Property(x => x.NameVi).IsRequired().HasMaxLength(200);
            b.Property(x => x.NameKo).HasMaxLength(200);
            b.Property(x => x.NameEn).HasMaxLength(200);
            b.Property(x => x.Description).HasMaxLength(1000);
            b.Property(x => x.ImageUrl).HasMaxLength(500);
            b.Property(x => x.Unit).IsRequired().HasMaxLength(50);
            b.Property(x => x.DefaultPrice).HasPrecision(18, 2);
            b.HasOne<MenuCategory>().WithMany().HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<BranchMenuItem>(b =>
        {
            b.ToTable("BranchMenuItems");
            b.ConfigureByConvention();
            b.HasIndex(x => new { x.BranchId, x.MenuItemId }).IsUnique();
            b.Property(x => x.Price).HasPrecision(18, 2);
            b.HasOne<MenuItem>().WithMany().HasForeignKey(x => x.MenuItemId).OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<MenuPrintTemplate>(b =>
        {
            b.ToTable("MenuPrintTemplates");
            b.ConfigureByConvention();
            b.Property(x => x.Name).IsRequired().HasMaxLength(150);
            b.Property(x => x.PaperSize).IsRequired().HasMaxLength(20);
            b.Property(x => x.LayoutType).IsRequired().HasMaxLength(50);
        });
    }
}
