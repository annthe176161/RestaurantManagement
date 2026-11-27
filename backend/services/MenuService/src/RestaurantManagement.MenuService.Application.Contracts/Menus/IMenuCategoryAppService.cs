using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace RestaurantManagement.MenuService.Menus;

public class MenuCategoryDto : AuditedEntityDto<Guid>
{
    public string NameVi { get; set; } = string.Empty;
    public string? NameKo { get; set; }
    public string? NameEn { get; set; }
    public int SortOrder { get; set; }
    public bool IsActive { get; set; }
}

public class CreateMenuCategoryDto
{
    public string NameVi { get; set; } = string.Empty;
    public string? NameKo { get; set; }
    public string? NameEn { get; set; }
    public int SortOrder { get; set; }
}

public interface IMenuCategoryAppService : IApplicationService
{
    Task<List<MenuCategoryDto>> GetListAsync();
    Task<MenuCategoryDto> CreateAsync(CreateMenuCategoryDto input);
    Task DeleteAsync(Guid id);
}
