using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.Modularity;

namespace RestaurantManagement.ReportingService.EntityFrameworkCore;

[DependsOn(typeof(ReportingServiceDomainModule), typeof(AbpEntityFrameworkCoreSqlServerModule))]
public class ReportingServiceEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDbContextOptions>(options => options.UseSqlServer());
    }
}
