using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.Modularity;

namespace RestaurantManagement.NotificationChatService.EntityFrameworkCore;

[DependsOn(typeof(NotificationChatServiceDomainModule), typeof(AbpEntityFrameworkCoreSqlServerModule))]
public class NotificationChatServiceEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDbContextOptions>(options => options.UseSqlServer());
    }
}
