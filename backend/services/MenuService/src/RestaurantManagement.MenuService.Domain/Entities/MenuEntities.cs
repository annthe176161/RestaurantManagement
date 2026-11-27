using Volo.Abp.Domain.Entities.Auditing;

namespace RestaurantManagement.MenuService.Entities;

public class MenuCategory : FullAuditedAggregateRoot<Guid>
{
    public string NameVi { get; set; } = string.Empty;
    public string? NameKo { get; set; }
    public string? NameEn { get; set; }
    public int SortOrder { get; set; }
    public bool IsActive { get; set; } = true;

    protected MenuCategory() { }
}

public class MenuItem : FullAuditedAggregateRoot<Guid>
{
    public Guid CategoryId { get; set; }
    public string Code { get; set; } = string.Empty;
    public string NameVi { get; set; } = string.Empty;
    public string? NameKo { get; set; }
    public string? NameEn { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public decimal DefaultPrice { get; set; }
    public string Unit { get; set; } = "Phần";
    public bool IsCombo { get; set; }
    public bool IsActive { get; set; } = true;

    protected MenuItem() { }
}

public class BranchMenuItem : FullAuditedAggregateRoot<Guid>
{
    public Guid BranchId { get; set; }
    public Guid MenuItemId { get; set; }
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; } = true;
    public bool IsFeatured { get; set; }

    protected BranchMenuItem() { }
}

public class MenuPrintTemplate : FullAuditedAggregateRoot<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string PaperSize { get; set; } = "A4";
    public string LayoutType { get; set; } = "Grid";
    public string? ConfigJson { get; set; }

    protected MenuPrintTemplate() { }
}
