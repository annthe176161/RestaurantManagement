using RestaurantManagement.Shared.Contracts.Enums;
using Volo.Abp.Domain.Entities.Auditing;

namespace RestaurantManagement.EmployeeService.Entities;

public class Employee : FullAuditedAggregateRoot<Guid>
{
    public Guid UserId { get; set; }
    public Guid BranchId { get; set; }
    public string EmployeeCode { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public StaffPosition Position { get; set; }
    public ContractType ContractType { get; set; }
    public decimal BaseSalary { get; set; }
    public decimal HourlyRate { get; set; }
    public DateTime HireDate { get; set; }
    public bool IsActive { get; set; } = true;

    protected Employee() { }

    public Employee(Guid id, Guid userId, Guid branchId, string employeeCode, string fullName, StaffPosition position)
        : base(id)
    {
        UserId = userId;
        BranchId = branchId;
        EmployeeCode = employeeCode;
        FullName = fullName;
        Position = position;
    }
}

public class ShiftTemplate : FullAuditedAggregateRoot<Guid>
{
    public Guid BranchId { get; set; }
    public string Name { get; set; } = string.Empty;
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public int BreakMinutes { get; set; }

    protected ShiftTemplate() { }

    public ShiftTemplate(Guid id, Guid branchId, string name, TimeSpan startTime, TimeSpan endTime)
        : base(id)
    {
        BranchId = branchId;
        Name = name;
        StartTime = startTime;
        EndTime = endTime;
    }
}

public class WeeklySchedule : FullAuditedAggregateRoot<Guid>
{
    public Guid BranchId { get; set; }
    public Guid EmployeeId { get; set; }
    public Guid ShiftTemplateId { get; set; }
    public DateTime WorkDate { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public ScheduleStatus Status { get; set; } = ScheduleStatus.Scheduled;
    public string? Note { get; set; }

    protected WeeklySchedule() { }
}

public class ShiftSwapRequest : FullAuditedAggregateRoot<Guid>
{
    public Guid ScheduleId { get; set; }
    public Guid FromEmployeeId { get; set; }
    public Guid? ToEmployeeId { get; set; }
    public string Reason { get; set; } = string.Empty;
    public ApprovalStatus Status { get; set; } = ApprovalStatus.Pending;
    public Guid? ReviewedByUserId { get; set; }

    protected ShiftSwapRequest() { }
}

public class Attendance : FullAuditedAggregateRoot<Guid>
{
    public Guid EmployeeId { get; set; }
    public Guid BranchId { get; set; }
    public Guid? ScheduleId { get; set; }
    public DateTime WorkDate { get; set; }
    public DateTime? CheckInTime { get; set; }
    public DateTime? CheckOutTime { get; set; }
    public CheckInMethod CheckInMethod { get; set; } = CheckInMethod.QrCode;
    public int LateMinutes { get; set; }
    public int EarlyLeaveMinutes { get; set; }
    public int OvertimeMinutes { get; set; }
    public decimal WorkedHours { get; set; }

    protected Attendance() { }
}

public class Payroll : FullAuditedAggregateRoot<Guid>
{
    public Guid EmployeeId { get; set; }
    public Guid BranchId { get; set; }
    public int Year { get; set; }
    public int Month { get; set; }
    public decimal TotalWorkedHours { get; set; }
    public decimal OvertimeHours { get; set; }
    public decimal BaseAmount { get; set; }
    public decimal OvertimeAmount { get; set; }
    public decimal Allowance { get; set; }
    public decimal Bonus { get; set; }
    public decimal Deduction { get; set; }
    public decimal NetSalary { get; set; }
    public PayrollStatus Status { get; set; } = PayrollStatus.Draft;

    protected Payroll() { }
}
