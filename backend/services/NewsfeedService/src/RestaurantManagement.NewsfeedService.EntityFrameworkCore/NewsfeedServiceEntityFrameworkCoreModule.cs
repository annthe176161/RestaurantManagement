using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.Modularity;

namespace RestaurantManagement.NewsfeedService.EntityFrameworkCore;

[DependsOn(typeof(NewsfeedServiceDomainModule), typeof(AbpEntityFrameworkCoreSqlServerModule))]
public class NewsfeedServiceEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDbContextOptions>(options => options.UseSqlServer());
    }
}
