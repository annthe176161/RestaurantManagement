using Volo.Abp.Modularity;
using Volo.Abp.Validation;

namespace RestaurantManagement.NewsfeedService;

[DependsOn(typeof(AbpValidationModule))]
public class NewsfeedServiceDomainSharedModule : AbpModule
{
}
