using RestaurantManagement.Shared.Contracts.Enums;
using Volo.Abp.Domain.Entities.Auditing;

namespace RestaurantManagement.BranchService.Entities;

public class Branch : FullAuditedAggregateRoot<Guid>
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? Email { get; set; }
    public TimeSpan OpenTime { get; set; }
    public TimeSpan CloseTime { get; set; }
    public int TotalTables { get; set; }
    public BranchStatus Status { get; set; } = BranchStatus.Active;
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public Guid? ManagerUserId { get; set; }

    public ICollection<BranchArea> Areas { get; set; } = new List<BranchArea>();

    protected Branch() { }

    public Branch(Guid id, string code, string name, string address, string phone)
        : base(id)
    {
        Code = code;
        Name = name;
        Address = address;
        Phone = phone;
    }
}

public class BranchArea : FullAuditedEntity<Guid>
{
    public Guid BranchId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int SortOrder { get; set; }
    public bool IsActive { get; set; } = true;

    protected BranchArea() { }

    public BranchArea(Guid id, Guid branchId, string name, int sortOrder = 0)
        : base(id)
    {
        BranchId = branchId;
        Name = name;
        SortOrder = sortOrder;
    }
}
