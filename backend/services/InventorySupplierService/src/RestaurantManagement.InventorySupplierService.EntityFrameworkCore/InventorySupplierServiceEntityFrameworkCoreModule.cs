using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.Modularity;

namespace RestaurantManagement.InventorySupplierService.EntityFrameworkCore;

[DependsOn(typeof(InventorySupplierServiceDomainModule), typeof(AbpEntityFrameworkCoreSqlServerModule))]
public class InventorySupplierServiceEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDbContextOptions>(options => options.UseSqlServer());
    }
}
