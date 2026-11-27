using Microsoft.EntityFrameworkCore;
using RestaurantManagement.AiOcrService.Entities;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace RestaurantManagement.AiOcrService.EntityFrameworkCore;

[ConnectionStringName("Default")]
public class AiOcrServiceDbContext : AbpDbContext<AiOcrServiceDbContext>
{
    public DbSet<OcrJob> OcrJobs => Set<OcrJob>();

    public AiOcrServiceDbContext(DbContextOptions<AiOcrServiceDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<OcrJob>(b =>
        {
            b.ToTable("OcrJobs");
            b.ConfigureByConvention();
            b.Property(x => x.FileUrl).IsRequired().HasMaxLength(500);
            b.Property(x => x.ConfidenceScore).HasPrecision(5, 4);
            b.Property(x => x.ErrorMessage).HasMaxLength(1000);
        });
    }
}
