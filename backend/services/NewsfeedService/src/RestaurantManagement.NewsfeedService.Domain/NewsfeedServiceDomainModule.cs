using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace RestaurantManagement.NewsfeedService;

[DependsOn(typeof(AbpDddDomainModule), typeof(NewsfeedServiceDomainSharedModule))]
public class NewsfeedServiceDomainModule : AbpModule
{
}
