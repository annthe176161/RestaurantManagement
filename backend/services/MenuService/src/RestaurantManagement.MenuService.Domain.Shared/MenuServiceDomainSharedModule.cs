using Volo.Abp.Modularity;
using Volo.Abp.Validation;

namespace RestaurantManagement.MenuService;

[DependsOn(typeof(AbpValidationModule))]
public class MenuServiceDomainSharedModule : AbpModule
{
}
