using AutoMapper;
using RestaurantManagement.BranchService.Branches;
using RestaurantManagement.BranchService.Entities;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace RestaurantManagement.BranchService.Branches;

public class BranchAutoMapperProfile : Profile
{
    public BranchAutoMapperProfile()
    {
        CreateMap<Branch, BranchDto>();
    }
}

public class BranchAppService : ApplicationService, IBranchAppService
{
    private readonly IRepository<Branch, Guid> _branchRepository;

    public BranchAppService(IRepository<Branch, Guid> branchRepository)
    {
        _branchRepository = branchRepository;
    }

    public async Task<PagedResultDto<BranchDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        var query = await _branchRepository.GetQueryableAsync();
        var totalCount = await AsyncExecuter.CountAsync(query);
        var items = await AsyncExecuter.ToListAsync(
            query.OrderBy(x => x.Name)
                 .Skip(input.SkipCount)
                 .Take(input.MaxResultCount)
        );

        return new PagedResultDto<BranchDto>(
            totalCount,
            ObjectMapper.Map<List<Branch>, List<BranchDto>>(items)
        );
    }

    public async Task<BranchDto> GetAsync(Guid id)
    {
        var branch = await _branchRepository.GetAsync(id);
        return ObjectMapper.Map<Branch, BranchDto>(branch);
    }

    public async Task<BranchDto> CreateAsync(CreateUpdateBranchDto input)
    {
        var branch = new Branch(
            GuidGenerator.Create(),
            input.Code,
            input.Name,
            input.Address,
            input.Phone
        )
        {
            Email = input.Email,
            OpenTime = input.OpenTime,
            CloseTime = input.CloseTime,
            TotalTables = input.TotalTables
        };

        await _branchRepository.InsertAsync(branch, autoSave: true);
        return ObjectMapper.Map<Branch, BranchDto>(branch);
    }

    public async Task<BranchDto> UpdateAsync(Guid id, CreateUpdateBranchDto input)
    {
        var branch = await _branchRepository.GetAsync(id);
        branch.Code = input.Code;
        branch.Name = input.Name;
        branch.Address = input.Address;
        branch.Phone = input.Phone;
        branch.Email = input.Email;
        branch.OpenTime = input.OpenTime;
        branch.CloseTime = input.CloseTime;
        branch.TotalTables = input.TotalTables;

        await _branchRepository.UpdateAsync(branch, autoSave: true);
        return ObjectMapper.Map<Branch, BranchDto>(branch);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _branchRepository.DeleteAsync(id);
    }
}
