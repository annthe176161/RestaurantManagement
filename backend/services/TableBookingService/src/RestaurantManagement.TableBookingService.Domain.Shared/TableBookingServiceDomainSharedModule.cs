using Volo.Abp.Modularity;
using Volo.Abp.Validation;

namespace RestaurantManagement.TableBookingService;

[DependsOn(typeof(AbpValidationModule))]
public class TableBookingServiceDomainSharedModule : AbpModule
{
}
