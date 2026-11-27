using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace RestaurantManagement.ReportingService;

[DependsOn(typeof(AbpDddDomainModule), typeof(ReportingServiceDomainSharedModule))]
public class ReportingServiceDomainModule : AbpModule
{
}
