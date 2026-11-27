using Microsoft.EntityFrameworkCore;
using RestaurantManagement.TableBookingService.Entities;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace RestaurantManagement.TableBookingService.EntityFrameworkCore;

[ConnectionStringName("Default")]
public class TableBookingServiceDbContext : AbpDbContext<TableBookingServiceDbContext>
{
    public DbSet<DiningTable> DiningTables => Set<DiningTable>();
    public DbSet<Reservation> Reservations => Set<Reservation>();

    public TableBookingServiceDbContext(DbContextOptions<TableBookingServiceDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<DiningTable>(b =>
        {
            b.ToTable("DiningTables");
            b.ConfigureByConvention();
            b.Property(x => x.TableCode).IsRequired().HasMaxLength(32);
            b.HasIndex(x => new { x.BranchId, x.TableCode }).IsUnique();
            b.Property(x => x.QrCodeToken).HasMaxLength(200);
        });

        builder.Entity<Reservation>(b =>
        {
            b.ToTable("Reservations");
            b.ConfigureByConvention();
            b.Property(x => x.BookingCode).IsRequired().HasMaxLength(32);
            b.HasIndex(x => x.BookingCode).IsUnique();
            b.Property(x => x.CustomerName).IsRequired().HasMaxLength(150);
            b.Property(x => x.CustomerPhone).IsRequired().HasMaxLength(32);
            b.Property(x => x.Note).HasMaxLength(500);
            b.HasOne<DiningTable>().WithMany().HasForeignKey(x => x.AssignedTableId).OnDelete(DeleteBehavior.SetNull);
        });
    }
}
