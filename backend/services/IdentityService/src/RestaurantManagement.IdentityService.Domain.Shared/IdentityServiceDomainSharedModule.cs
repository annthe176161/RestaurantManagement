using Volo.Abp.Modularity;
using Volo.Abp.Validation;

namespace RestaurantManagement.IdentityService;

[DependsOn(typeof(AbpValidationModule))]
public class IdentityServiceDomainSharedModule : AbpModule
{
}
