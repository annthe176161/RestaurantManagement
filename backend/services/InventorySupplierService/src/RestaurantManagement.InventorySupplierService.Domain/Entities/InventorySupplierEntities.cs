using RestaurantManagement.Shared.Contracts.Enums;
using Volo.Abp.Domain.Entities.Auditing;

namespace RestaurantManagement.InventorySupplierService.Entities;

public class Supplier : FullAuditedAggregateRoot<Guid>
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? TaxCode { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? ContactPerson { get; set; }

    protected Supplier() { }
}

public class Ingredient : FullAuditedAggregateRoot<Guid>
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Unit { get; set; } = "kg";
    public decimal MinStockThreshold { get; set; }

    protected Ingredient() { }
}

public class BranchInventory : FullAuditedAggregateRoot<Guid>
{
    public Guid BranchId { get; set; }
    public Guid IngredientId { get; set; }
    public decimal CurrentQuantity { get; set; }
    public DateTime LastUpdatedAt { get; set; }

    protected BranchInventory() { }
}

public class MenuItemRecipe : FullAuditedAggregateRoot<Guid>
{
    public Guid MenuItemId { get; set; }
    public Guid IngredientId { get; set; }
    public decimal QuantityRequired { get; set; }

    protected MenuItemRecipe() { }
}

public class PurchaseInvoice : FullAuditedAggregateRoot<Guid>
{
    public string InvoiceNumber { get; set; } = string.Empty;
    public Guid BranchId { get; set; }
    public Guid? SupplierId { get; set; }
    public string? SupplierNameSnapshot { get; set; }
    public DateTime InvoiceDate { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal VatAmount { get; set; }
    public string? ImageUrl { get; set; }
    public InvoiceSource Source { get; set; } = InvoiceSource.Manual;
    public PurchaseInvoiceStatus Status { get; set; } = PurchaseInvoiceStatus.Draft;

    public ICollection<PurchaseInvoiceItem> Items { get; set; } = new List<PurchaseInvoiceItem>();

    protected PurchaseInvoice() { }
}

public class PurchaseInvoiceItem : FullAuditedEntity<Guid>
{
    public Guid PurchaseInvoiceId { get; set; }
    public Guid? IngredientId { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public string Unit { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public decimal LineTotal { get; set; }

    protected PurchaseInvoiceItem() { }
}
