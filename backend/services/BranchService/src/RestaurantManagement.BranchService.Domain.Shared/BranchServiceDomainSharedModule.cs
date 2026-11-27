using Volo.Abp.Modularity;
using Volo.Abp.Validation;

namespace RestaurantManagement.BranchService;

[DependsOn(typeof(AbpValidationModule))]
public class BranchServiceDomainSharedModule : AbpModule
{
}
