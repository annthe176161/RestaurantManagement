using Volo.Abp.Modularity;
using Volo.Abp.Validation;

namespace RestaurantManagement.PaymentService;

[DependsOn(typeof(AbpValidationModule))]
public class PaymentServiceDomainSharedModule : AbpModule
{
}
