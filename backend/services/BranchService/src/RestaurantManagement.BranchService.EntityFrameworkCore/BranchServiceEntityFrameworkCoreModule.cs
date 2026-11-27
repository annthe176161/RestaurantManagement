using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.Modularity;

namespace RestaurantManagement.BranchService.EntityFrameworkCore;

[DependsOn(typeof(BranchServiceDomainModule), typeof(AbpEntityFrameworkCoreSqlServerModule))]
public class BranchServiceEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDbContextOptions>(options => options.UseSqlServer());
    }
}
