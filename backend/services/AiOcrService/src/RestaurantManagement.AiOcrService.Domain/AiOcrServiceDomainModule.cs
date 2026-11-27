using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace RestaurantManagement.AiOcrService;

[DependsOn(typeof(AbpDddDomainModule), typeof(AiOcrServiceDomainSharedModule))]
public class AiOcrServiceDomainModule : AbpModule
{
}
