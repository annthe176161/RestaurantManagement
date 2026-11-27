using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.Modularity;

namespace RestaurantManagement.MenuService.EntityFrameworkCore;

[DependsOn(typeof(MenuServiceDomainModule), typeof(AbpEntityFrameworkCoreSqlServerModule))]
public class MenuServiceEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<MenuServiceDbContext>(options =>
        {
            options.AddDefaultRepositories(includeAllEntities: true);
        });

        Configure<AbpDbContextOptions>(options => options.UseSqlServer());
    }
}
