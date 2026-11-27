using RestaurantManagement.Shared.Contracts.Enums;
using Volo.Abp.Domain.Entities.Auditing;

namespace RestaurantManagement.OrderService.Entities;

public class Order : FullAuditedAggregateRoot<Guid>
{
    public string OrderCode { get; set; } = string.Empty;
    public Guid BranchId { get; set; }
    public Guid? TableId { get; set; }
    public OrderType OrderType { get; set; } = OrderType.DineIn;
    public Guid? CreatedByEmployeeId { get; set; }
    public Guid? CustomerUserId { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Open;
    public decimal SubTotal { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal VatAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public string? Note { get; set; }

    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();

    protected Order() { }
}

public class OrderItem : FullAuditedEntity<Guid>
{
    public Guid OrderId { get; set; }
    public Guid MenuItemId { get; set; }
    public string MenuItemName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal LineTotal { get; set; }
    public KitchenItemStatus KitchenStatus { get; set; } = KitchenItemStatus.Pending;
    public string? Note { get; set; }

    protected OrderItem() { }
}
