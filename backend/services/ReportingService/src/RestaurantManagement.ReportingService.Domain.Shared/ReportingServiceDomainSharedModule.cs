using Volo.Abp.Modularity;
using Volo.Abp.Validation;

namespace RestaurantManagement.ReportingService;

[DependsOn(typeof(AbpValidationModule))]
public class ReportingServiceDomainSharedModule : AbpModule
{
}
