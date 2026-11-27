using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace RestaurantManagement.MenuService;

[DependsOn(
    typeof(AbpDddApplicationContractsModule),
    typeof(MenuServiceDomainSharedModule)
)]
public class MenuServiceApplicationContractsModule : AbpModule
{
}
