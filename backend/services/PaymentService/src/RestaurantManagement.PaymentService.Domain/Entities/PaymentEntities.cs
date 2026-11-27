using RestaurantManagement.Shared.Contracts.Enums;
using Volo.Abp.Domain.Entities.Auditing;

namespace RestaurantManagement.PaymentService.Entities;

public class SalesInvoice : FullAuditedAggregateRoot<Guid>
{
    public string InvoiceNumber { get; set; } = string.Empty;
    public Guid BranchId { get; set; }
    public Guid OrderId { get; set; }
    public DateTime IssuedAt { get; set; }
    public decimal SubTotal { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal VatRate { get; set; }
    public decimal VatAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Unpaid;
    public string? PdfUrl { get; set; }

    public ICollection<PaymentTransaction> Transactions { get; set; } = new List<PaymentTransaction>();

    protected SalesInvoice() { }
}

public class PaymentTransaction : FullAuditedEntity<Guid>
{
    public Guid SalesInvoiceId { get; set; }
    public PaymentMethod Method { get; set; }
    public decimal Amount { get; set; }
    public string? TransactionRef { get; set; }
    public DateTime PaidAt { get; set; }
    public Guid CashierUserId { get; set; }

    protected PaymentTransaction() { }
}
