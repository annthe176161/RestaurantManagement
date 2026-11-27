using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace RestaurantManagement.BranchService;

[DependsOn(
    typeof(AbpDddApplicationContractsModule),
    typeof(BranchServiceDomainSharedModule)
)]
public class BranchServiceApplicationContractsModule : AbpModule
{
}
