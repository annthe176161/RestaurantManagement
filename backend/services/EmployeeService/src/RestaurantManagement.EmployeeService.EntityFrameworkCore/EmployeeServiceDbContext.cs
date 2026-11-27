using Microsoft.EntityFrameworkCore;
using RestaurantManagement.EmployeeService.Entities;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace RestaurantManagement.EmployeeService.EntityFrameworkCore;

[ConnectionStringName("Default")]
public class EmployeeServiceDbContext : AbpDbContext<EmployeeServiceDbContext>
{
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<ShiftTemplate> ShiftTemplates => Set<ShiftTemplate>();
    public DbSet<WeeklySchedule> WeeklySchedules => Set<WeeklySchedule>();
    public DbSet<ShiftSwapRequest> ShiftSwapRequests => Set<ShiftSwapRequest>();
    public DbSet<Attendance> Attendances => Set<Attendance>();
    public DbSet<Payroll> Payrolls => Set<Payroll>();

    public EmployeeServiceDbContext(DbContextOptions<EmployeeServiceDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Employee>(b =>
        {
            b.ToTable("Employees");
            b.ConfigureByConvention();
            b.Property(x => x.EmployeeCode).IsRequired().HasMaxLength(32);
            b.HasIndex(x => x.EmployeeCode).IsUnique();
            b.Property(x => x.FullName).IsRequired().HasMaxLength(150);
            b.Property(x => x.BaseSalary).HasPrecision(18, 2);
            b.Property(x => x.HourlyRate).HasPrecision(18, 2);
        });

        builder.Entity<ShiftTemplate>(b =>
        {
            b.ToTable("ShiftTemplates");
            b.ConfigureByConvention();
            b.Property(x => x.Name).IsRequired().HasMaxLength(100);
        });

        builder.Entity<WeeklySchedule>(b =>
        {
            b.ToTable("WeeklySchedules");
            b.ConfigureByConvention();
            b.Property(x => x.Note).HasMaxLength(500);
            b.HasOne<Employee>().WithMany().HasForeignKey(x => x.EmployeeId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne<ShiftTemplate>().WithMany().HasForeignKey(x => x.ShiftTemplateId).OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<ShiftSwapRequest>(b =>
        {
            b.ToTable("ShiftSwapRequests");
            b.ConfigureByConvention();
            b.Property(x => x.Reason).IsRequired().HasMaxLength(500);
            b.HasOne<WeeklySchedule>().WithMany().HasForeignKey(x => x.ScheduleId).OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<Attendance>(b =>
        {
            b.ToTable("Attendances");
            b.ConfigureByConvention();
            b.Property(x => x.WorkedHours).HasPrecision(8, 2);
            b.HasOne<Employee>().WithMany().HasForeignKey(x => x.EmployeeId).OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<Payroll>(b =>
        {
            b.ToTable("Payrolls");
            b.ConfigureByConvention();
            b.HasIndex(x => new { x.EmployeeId, x.Year, x.Month }).IsUnique();
            b.Property(x => x.TotalWorkedHours).HasPrecision(10, 2);
            b.Property(x => x.OvertimeHours).HasPrecision(10, 2);
            b.Property(x => x.BaseAmount).HasPrecision(18, 2);
            b.Property(x => x.OvertimeAmount).HasPrecision(18, 2);
            b.Property(x => x.Allowance).HasPrecision(18, 2);
            b.Property(x => x.Bonus).HasPrecision(18, 2);
            b.Property(x => x.Deduction).HasPrecision(18, 2);
            b.Property(x => x.NetSalary).HasPrecision(18, 2);
            b.HasOne<Employee>().WithMany().HasForeignKey(x => x.EmployeeId).OnDelete(DeleteBehavior.Restrict);
        });
    }
}
