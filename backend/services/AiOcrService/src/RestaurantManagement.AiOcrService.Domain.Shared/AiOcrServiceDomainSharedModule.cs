using Volo.Abp.Modularity;
using Volo.Abp.Validation;

namespace RestaurantManagement.AiOcrService;

[DependsOn(typeof(AbpValidationModule))]
public class AiOcrServiceDomainSharedModule : AbpModule
{
}
