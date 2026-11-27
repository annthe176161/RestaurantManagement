using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;

namespace RestaurantManagement.BranchService;

[DependsOn(
    typeof(AbpAspNetCoreMvcModule),
    typeof(BranchServiceApplicationContractsModule)
)]
public class BranchServiceHttpApiModule : AbpModule
{
}
