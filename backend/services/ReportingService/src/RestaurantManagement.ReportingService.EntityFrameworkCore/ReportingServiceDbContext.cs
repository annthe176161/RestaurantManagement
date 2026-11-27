using Microsoft.EntityFrameworkCore;
using RestaurantManagement.ReportingService.Entities;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace RestaurantManagement.ReportingService.EntityFrameworkCore;

[ConnectionStringName("Default")]
public class ReportingServiceDbContext : AbpDbContext<ReportingServiceDbContext>
{
    public DbSet<DailyBranchRevenue> DailyBranchRevenues => Set<DailyBranchRevenue>();
    public DbSet<MonthlyExportLog> MonthlyExportLogs => Set<MonthlyExportLog>();

    public ReportingServiceDbContext(DbContextOptions<ReportingServiceDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<DailyBranchRevenue>(b =>
        {
            b.ToTable("DailyBranchRevenues");
            b.ConfigureByConvention();
            b.HasIndex(x => new { x.BranchId, x.Date }).IsUnique();
            b.Property(x => x.GrossRevenue).HasPrecision(18, 2);
            b.Property(x => x.DiscountTotal).HasPrecision(18, 2);
            b.Property(x => x.VatTotal).HasPrecision(18, 2);
            b.Property(x => x.NetRevenue).HasPrecision(18, 2);
            b.Property(x => x.PurchaseCost).HasPrecision(18, 2);
            b.Property(x => x.PayrollCost).HasPrecision(18, 2);
            b.Property(x => x.EstimatedProfit).HasPrecision(18, 2);
        });

        builder.Entity<MonthlyExportLog>(b =>
        {
            b.ToTable("MonthlyExportLogs");
            b.ConfigureByConvention();
            b.Property(x => x.FileUrl).IsRequired().HasMaxLength(500);
        });
    }
}
