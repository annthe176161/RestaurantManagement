using RestaurantManagement.Shared.Contracts.Enums;
using Volo.Abp.Domain.Entities.Auditing;

namespace RestaurantManagement.AiOcrService.Entities;

public class OcrJob : FullAuditedAggregateRoot<Guid>
{
    public Guid BranchId { get; set; }
    public Guid UploadedByUserId { get; set; }
    public string FileUrl { get; set; } = string.Empty;
    public OcrFileType FileType { get; set; } = OcrFileType.Image;
    public OcrDocumentType DocumentType { get; set; } = OcrDocumentType.PurchaseInvoice;
    public OcrJobStatus Status { get; set; } = OcrJobStatus.Pending;
    public string? RawResponseJson { get; set; }
    public string? ExtractedTableJson { get; set; }
    public decimal? ConfidenceScore { get; set; }
    public string? ErrorMessage { get; set; }
    public DateTime? ProcessedAt { get; set; }

    protected OcrJob() { }
}
