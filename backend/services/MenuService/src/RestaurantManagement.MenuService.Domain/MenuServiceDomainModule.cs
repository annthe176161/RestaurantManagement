using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace RestaurantManagement.MenuService;

[DependsOn(typeof(AbpDddDomainModule), typeof(MenuServiceDomainSharedModule))]
public class MenuServiceDomainModule : AbpModule
{
}
