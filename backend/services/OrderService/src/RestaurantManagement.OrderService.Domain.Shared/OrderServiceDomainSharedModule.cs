using Volo.Abp.Modularity;
using Volo.Abp.Validation;

namespace RestaurantManagement.OrderService;

[DependsOn(typeof(AbpValidationModule))]
public class OrderServiceDomainSharedModule : AbpModule
{
}
