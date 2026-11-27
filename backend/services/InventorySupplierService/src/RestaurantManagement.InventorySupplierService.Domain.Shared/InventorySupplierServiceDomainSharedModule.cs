using Volo.Abp.Modularity;
using Volo.Abp.Validation;

namespace RestaurantManagement.InventorySupplierService;

[DependsOn(typeof(AbpValidationModule))]
public class InventorySupplierServiceDomainSharedModule : AbpModule
{
}
