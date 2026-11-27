using RestaurantManagement.Shared.Contracts.Enums;
using Volo.Abp.Domain.Entities.Auditing;

namespace RestaurantManagement.ReportingService.Entities;

public class DailyBranchRevenue : FullAuditedAggregateRoot<Guid>
{
    public Guid BranchId { get; set; }
    public DateTime Date { get; set; }
    public int TotalOrders { get; set; }
    public decimal GrossRevenue { get; set; }
    public decimal DiscountTotal { get; set; }
    public decimal VatTotal { get; set; }
    public decimal NetRevenue { get; set; }
    public decimal PurchaseCost { get; set; }
    public decimal PayrollCost { get; set; }
    public decimal EstimatedProfit { get; set; }

    protected DailyBranchRevenue() { }
}

public class MonthlyExportLog : FullAuditedAggregateRoot<Guid>
{
    public int Year { get; set; }
    public int Month { get; set; }
    public Guid? BranchId { get; set; }
    public ReportType ReportType { get; set; }
    public string FileUrl { get; set; } = string.Empty;
    public Guid GeneratedByUserId { get; set; }
    public DateTime GeneratedAt { get; set; }

    protected MonthlyExportLog() { }
}
