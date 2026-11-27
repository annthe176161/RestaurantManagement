using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.Modularity;

namespace RestaurantManagement.OrderService.EntityFrameworkCore;

[DependsOn(typeof(OrderServiceDomainModule), typeof(AbpEntityFrameworkCoreSqlServerModule))]
public class OrderServiceEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDbContextOptions>(options => options.UseSqlServer());
    }
}
