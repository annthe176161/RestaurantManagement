using Microsoft.EntityFrameworkCore;
using RestaurantManagement.PaymentService.Entities;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace RestaurantManagement.PaymentService.EntityFrameworkCore;

[ConnectionStringName("Default")]
public class PaymentServiceDbContext : AbpDbContext<PaymentServiceDbContext>
{
    public DbSet<SalesInvoice> SalesInvoices => Set<SalesInvoice>();
    public DbSet<PaymentTransaction> PaymentTransactions => Set<PaymentTransaction>();

    public PaymentServiceDbContext(DbContextOptions<PaymentServiceDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<SalesInvoice>(b =>
        {
            b.ToTable("SalesInvoices");
            b.ConfigureByConvention();
            b.Property(x => x.InvoiceNumber).IsRequired().HasMaxLength(50);
            b.HasIndex(x => x.InvoiceNumber).IsUnique();
            b.Property(x => x.SubTotal).HasPrecision(18, 2);
            b.Property(x => x.DiscountAmount).HasPrecision(18, 2);
            b.Property(x => x.VatRate).HasPrecision(5, 2);
            b.Property(x => x.VatAmount).HasPrecision(18, 2);
            b.Property(x => x.TotalAmount).HasPrecision(18, 2);
            b.Property(x => x.PdfUrl).HasMaxLength(500);
            b.HasMany(x => x.Transactions).WithOne().HasForeignKey(x => x.SalesInvoiceId).OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<PaymentTransaction>(b =>
        {
            b.ToTable("PaymentTransactions");
            b.ConfigureByConvention();
            b.Property(x => x.Amount).HasPrecision(18, 2);
            b.Property(x => x.TransactionRef).HasMaxLength(150);
        });
    }
}
