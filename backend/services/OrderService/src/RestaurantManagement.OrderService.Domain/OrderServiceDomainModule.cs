using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace RestaurantManagement.OrderService;

[DependsOn(typeof(AbpDddDomainModule), typeof(OrderServiceDomainSharedModule))]
public class OrderServiceDomainModule : AbpModule
{
}
