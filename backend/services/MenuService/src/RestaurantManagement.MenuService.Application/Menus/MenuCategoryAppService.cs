using AutoMapper;
using RestaurantManagement.MenuService.Entities;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace RestaurantManagement.MenuService.Menus;

public class MenuAutoMapperProfile : Profile
{
    public MenuAutoMapperProfile()
    {
        CreateMap<MenuCategory, MenuCategoryDto>();
    }
}

public class MenuCategoryAppService : ApplicationService, IMenuCategoryAppService
{
    private readonly IRepository<MenuCategory, Guid> _categoryRepository;

    public MenuCategoryAppService(IRepository<MenuCategory, Guid> categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<List<MenuCategoryDto>> GetListAsync()
    {
        var items = await _categoryRepository.GetListAsync();
        return ObjectMapper.Map<List<MenuCategory>, List<MenuCategoryDto>>(
            items.OrderBy(x => x.SortOrder).ToList()
        );
    }

    public async Task<MenuCategoryDto> CreateAsync(CreateMenuCategoryDto input)
    {
        var category = new MenuCategory(
            GuidGenerator.Create(),
            input.NameVi,
            input.NameKo,
            input.NameEn,
            input.SortOrder
        );

        await _categoryRepository.InsertAsync(category, autoSave: true);
        return ObjectMapper.Map<MenuCategory, MenuCategoryDto>(category);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _categoryRepository.DeleteAsync(id);
    }
}
