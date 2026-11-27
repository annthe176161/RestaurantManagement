using RestaurantManagement.Shared.Contracts.Enums;
using Volo.Abp.Domain.Entities.Auditing;

namespace RestaurantManagement.TableBookingService.Entities;

public class DiningTable : FullAuditedAggregateRoot<Guid>
{
    public Guid BranchId { get; set; }
    public Guid AreaId { get; set; }
    public string TableCode { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public TableStatus Status { get; set; } = TableStatus.Available;
    public string? QrCodeToken { get; set; }

    protected DiningTable() { }
}

public class Reservation : FullAuditedAggregateRoot<Guid>
{
    public string BookingCode { get; set; } = string.Empty;
    public Guid BranchId { get; set; }
    public Guid? CustomerUserId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerPhone { get; set; } = string.Empty;
    public DateTime ReservationTime { get; set; }
    public int GuestCount { get; set; }
    public Guid? PreferredAreaId { get; set; }
    public Guid? AssignedTableId { get; set; }
    public string? Note { get; set; }
    public ReservationStatus Status { get; set; } = ReservationStatus.Pending;
    public Guid? ConfirmedByUserId { get; set; }

    protected Reservation() { }
}
