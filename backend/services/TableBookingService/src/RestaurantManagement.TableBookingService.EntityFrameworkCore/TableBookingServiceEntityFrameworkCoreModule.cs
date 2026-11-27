using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.Modularity;

namespace RestaurantManagement.TableBookingService.EntityFrameworkCore;

[DependsOn(typeof(TableBookingServiceDomainModule), typeof(AbpEntityFrameworkCoreSqlServerModule))]
public class TableBookingServiceEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDbContextOptions>(options => options.UseSqlServer());
    }
}
