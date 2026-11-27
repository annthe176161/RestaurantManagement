using Microsoft.EntityFrameworkCore;
using RestaurantManagement.BranchService.Entities;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace RestaurantManagement.BranchService.EntityFrameworkCore;

[ConnectionStringName("Default")]
public class BranchServiceDbContext : AbpDbContext<BranchServiceDbContext>
{
    public DbSet<Branch> Branches => Set<Branch>();
    public DbSet<BranchArea> BranchAreas => Set<BranchArea>();

    public BranchServiceDbContext(DbContextOptions<BranchServiceDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Branch>(b =>
        {
            b.ToTable("Branches");
            b.ConfigureByConvention();
            b.Property(x => x.Code).IsRequired().HasMaxLength(32);
            b.HasIndex(x => x.Code).IsUnique();
            b.Property(x => x.Name).IsRequired().HasMaxLength(200);
            b.Property(x => x.Address).IsRequired().HasMaxLength(500);
            b.Property(x => x.Phone).IsRequired().HasMaxLength(32);
            b.Property(x => x.Email).HasMaxLength(200);
            b.HasMany(x => x.Areas).WithOne().HasForeignKey(x => x.BranchId).OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<BranchArea>(b =>
        {
            b.ToTable("BranchAreas");
            b.ConfigureByConvention();
            b.Property(x => x.Name).IsRequired().HasMaxLength(150);
            b.Property(x => x.Description).HasMaxLength(500);
        });
    }
}
