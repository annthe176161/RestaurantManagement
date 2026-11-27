using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.Modularity;

namespace RestaurantManagement.BranchService.EntityFrameworkCore;

[DependsOn(typeof(BranchServiceDomainModule), typeof(AbpEntityFrameworkCoreSqlServerModule))]
public class BranchServiceEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<BranchServiceDbContext>(options =>
        {
            options.AddDefaultRepositories(includeAllEntities: true);
        });

        Configure<AbpDbContextOptions>(options => options.UseSqlServer());
    }
}
