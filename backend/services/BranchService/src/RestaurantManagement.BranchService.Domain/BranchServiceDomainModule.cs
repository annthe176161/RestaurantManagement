using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace RestaurantManagement.BranchService;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(BranchServiceDomainSharedModule)
)]
public class BranchServiceDomainModule : AbpModule
{
}
