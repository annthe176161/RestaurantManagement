using Microsoft.EntityFrameworkCore;
using RestaurantManagement.InventorySupplierService.Entities;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace RestaurantManagement.InventorySupplierService.EntityFrameworkCore;

[ConnectionStringName("Default")]
public class InventorySupplierServiceDbContext : AbpDbContext<InventorySupplierServiceDbContext>
{
    public DbSet<Supplier> Suppliers => Set<Supplier>();
    public DbSet<Ingredient> Ingredients => Set<Ingredient>();
    public DbSet<BranchInventory> BranchInventories => Set<BranchInventory>();
    public DbSet<MenuItemRecipe> MenuItemRecipes => Set<MenuItemRecipe>();
    public DbSet<PurchaseInvoice> PurchaseInvoices => Set<PurchaseInvoice>();
    public DbSet<PurchaseInvoiceItem> PurchaseInvoiceItems => Set<PurchaseInvoiceItem>();

    public InventorySupplierServiceDbContext(DbContextOptions<InventorySupplierServiceDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Supplier>(b =>
        {
            b.ToTable("Suppliers");
            b.ConfigureByConvention();
            b.Property(x => x.Code).IsRequired().HasMaxLength(32);
            b.HasIndex(x => x.Code).IsUnique();
            b.Property(x => x.Name).IsRequired().HasMaxLength(200);
            b.Property(x => x.TaxCode).HasMaxLength(50);
            b.Property(x => x.Phone).HasMaxLength(32);
            b.Property(x => x.Address).HasMaxLength(500);
            b.Property(x => x.ContactPerson).HasMaxLength(150);
        });

        builder.Entity<Ingredient>(b =>
        {
            b.ToTable("Ingredients");
            b.ConfigureByConvention();
            b.Property(x => x.Code).IsRequired().HasMaxLength(32);
            b.HasIndex(x => x.Code).IsUnique();
            b.Property(x => x.Name).IsRequired().HasMaxLength(200);
            b.Property(x => x.Unit).IsRequired().HasMaxLength(32);
            b.Property(x => x.MinStockThreshold).HasPrecision(18, 3);
        });

        builder.Entity<BranchInventory>(b =>
        {
            b.ToTable("BranchInventories");
            b.ConfigureByConvention();
            b.HasIndex(x => new { x.BranchId, x.IngredientId }).IsUnique();
            b.Property(x => x.CurrentQuantity).HasPrecision(18, 3);
            b.HasOne<Ingredient>().WithMany().HasForeignKey(x => x.IngredientId).OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<MenuItemRecipe>(b =>
        {
            b.ToTable("MenuItemRecipes");
            b.ConfigureByConvention();
            b.HasIndex(x => new { x.MenuItemId, x.IngredientId }).IsUnique();
            b.Property(x => x.QuantityRequired).HasPrecision(18, 4);
            b.HasOne<Ingredient>().WithMany().HasForeignKey(x => x.IngredientId).OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<PurchaseInvoice>(b =>
        {
            b.ToTable("PurchaseInvoices");
            b.ConfigureByConvention();
            b.Property(x => x.InvoiceNumber).IsRequired().HasMaxLength(50);
            b.Property(x => x.SupplierNameSnapshot).HasMaxLength(200);
            b.Property(x => x.TotalAmount).HasPrecision(18, 2);
            b.Property(x => x.VatAmount).HasPrecision(18, 2);
            b.Property(x => x.ImageUrl).HasMaxLength(500);
            b.HasOne<Supplier>().WithMany().HasForeignKey(x => x.SupplierId).OnDelete(DeleteBehavior.SetNull);
            b.HasMany(x => x.Items).WithOne().HasForeignKey(x => x.PurchaseInvoiceId).OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<PurchaseInvoiceItem>(b =>
        {
            b.ToTable("PurchaseInvoiceItems");
            b.ConfigureByConvention();
            b.Property(x => x.ItemName).IsRequired().HasMaxLength(200);
            b.Property(x => x.Unit).IsRequired().HasMaxLength(32);
            b.Property(x => x.Quantity).HasPrecision(18, 3);
            b.Property(x => x.UnitPrice).HasPrecision(18, 2);
            b.Property(x => x.LineTotal).HasPrecision(18, 2);
            b.HasOne<Ingredient>().WithMany().HasForeignKey(x => x.IngredientId).OnDelete(DeleteBehavior.SetNull);
        });
    }
}
