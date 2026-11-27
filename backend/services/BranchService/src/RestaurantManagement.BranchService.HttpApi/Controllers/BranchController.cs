using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.BranchService.Branches;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace RestaurantManagement.BranchService.Controllers;

[ApiController]
[Route("api/branches")]
public class BranchController : AbpControllerBase, IBranchAppService
{
    private readonly IBranchAppService _branchAppService;

    public BranchController(IBranchAppService branchAppService)
    {
        _branchAppService = branchAppService;
    }

    [HttpGet]
    public Task<PagedResultDto<BranchDto>> GetListAsync([FromQuery] PagedAndSortedResultRequestDto input)
        => _branchAppService.GetListAsync(input);

    [HttpGet("{id:guid}")]
    public Task<BranchDto> GetAsync(Guid id)
        => _branchAppService.GetAsync(id);

    [HttpPost]
    public Task<BranchDto> CreateAsync([FromBody] CreateUpdateBranchDto input)
        => _branchAppService.CreateAsync(input);

    [HttpPut("{id:guid}")]
    public Task<BranchDto> UpdateAsync(Guid id, [FromBody] CreateUpdateBranchDto input)
        => _branchAppService.UpdateAsync(id, input);

    [HttpDelete("{id:guid}")]
    public Task DeleteAsync(Guid id)
        => _branchAppService.DeleteAsync(id);
}
