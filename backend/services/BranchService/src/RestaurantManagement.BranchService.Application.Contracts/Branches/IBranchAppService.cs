using RestaurantManagement.Shared.Contracts.Enums;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace RestaurantManagement.BranchService.Branches;

public class BranchDto : AuditedEntityDto<Guid>
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? Email { get; set; }
    public TimeSpan OpenTime { get; set; }
    public TimeSpan CloseTime { get; set; }
    public int TotalTables { get; set; }
    public BranchStatus Status { get; set; }
}

public class CreateUpdateBranchDto
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? Email { get; set; }
    public TimeSpan OpenTime { get; set; } = new(10, 0, 0);
    public TimeSpan CloseTime { get; set; } = new(22, 0, 0);
    public int TotalTables { get; set; } = 20;
}

public interface IBranchAppService : IApplicationService
{
    Task<PagedResultDto<BranchDto>> GetListAsync(PagedAndSortedResultRequestDto input);
    Task<BranchDto> GetAsync(Guid id);
    Task<BranchDto> CreateAsync(CreateUpdateBranchDto input);
    Task<BranchDto> UpdateAsync(Guid id, CreateUpdateBranchDto input);
    Task DeleteAsync(Guid id);
}
