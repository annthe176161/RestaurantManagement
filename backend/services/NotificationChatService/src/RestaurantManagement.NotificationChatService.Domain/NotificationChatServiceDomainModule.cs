using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace RestaurantManagement.NotificationChatService;

[DependsOn(typeof(AbpDddDomainModule), typeof(NotificationChatServiceDomainSharedModule))]
public class NotificationChatServiceDomainModule : AbpModule
{
}
