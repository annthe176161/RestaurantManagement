using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace RestaurantManagement.TableBookingService;

[DependsOn(typeof(AbpDddDomainModule), typeof(TableBookingServiceDomainSharedModule))]
public class TableBookingServiceDomainModule : AbpModule
{
}
