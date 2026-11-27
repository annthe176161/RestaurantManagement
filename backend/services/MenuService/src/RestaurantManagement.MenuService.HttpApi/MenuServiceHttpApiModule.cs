using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;

namespace RestaurantManagement.MenuService;

[DependsOn(
    typeof(AbpAspNetCoreMvcModule),
    typeof(MenuServiceApplicationContractsModule)
)]
public class MenuServiceHttpApiModule : AbpModule
{
}
