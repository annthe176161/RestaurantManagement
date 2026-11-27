using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.Modularity;

namespace RestaurantManagement.EmployeeService.EntityFrameworkCore;

[DependsOn(typeof(EmployeeServiceDomainModule), typeof(AbpEntityFrameworkCoreSqlServerModule))]
public class EmployeeServiceEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDbContextOptions>(options => options.UseSqlServer());
    }
}
