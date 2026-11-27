using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace RestaurantManagement.InventorySupplierService;

[DependsOn(typeof(AbpDddDomainModule), typeof(InventorySupplierServiceDomainSharedModule))]
public class InventorySupplierServiceDomainModule : AbpModule
{
}
