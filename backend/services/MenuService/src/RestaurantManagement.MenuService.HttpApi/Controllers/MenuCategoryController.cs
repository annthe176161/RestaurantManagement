using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.MenuService.Menus;
using Volo.Abp.AspNetCore.Mvc;

namespace RestaurantManagement.MenuService.Controllers;

[ApiController]
[Route("api/menu-categories")]
public class MenuCategoryController : AbpControllerBase, IMenuCategoryAppService
{
    private readonly IMenuCategoryAppService _categoryAppService;

    public MenuCategoryController(IMenuCategoryAppService categoryAppService)
    {
        _categoryAppService = categoryAppService;
    }

    [HttpGet]
    public Task<List<MenuCategoryDto>> GetListAsync()
        => _categoryAppService.GetListAsync();

    [HttpPost]
    public Task<MenuCategoryDto> CreateAsync([FromBody] CreateMenuCategoryDto input)
        => _categoryAppService.CreateAsync(input);

    [HttpDelete("{id:guid}")]
    public Task DeleteAsync(Guid id)
        => _categoryAppService.DeleteAsync(id);
}
