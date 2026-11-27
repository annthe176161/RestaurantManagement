using Volo.Abp.Modularity;
using Volo.Abp.Validation;

namespace RestaurantManagement.NotificationChatService;

[DependsOn(typeof(AbpValidationModule))]
public class NotificationChatServiceDomainSharedModule : AbpModule
{
}
