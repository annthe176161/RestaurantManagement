using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.Modularity;

namespace RestaurantManagement.AiOcrService.EntityFrameworkCore;

[DependsOn(typeof(AiOcrServiceDomainModule), typeof(AbpEntityFrameworkCoreSqlServerModule))]
public class AiOcrServiceEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDbContextOptions>(options => options.UseSqlServer());
    }
}
