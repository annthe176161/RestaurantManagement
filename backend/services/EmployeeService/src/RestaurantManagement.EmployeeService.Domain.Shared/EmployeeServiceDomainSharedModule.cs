using Volo.Abp.Modularity;
using Volo.Abp.Validation;

namespace RestaurantManagement.EmployeeService;

[DependsOn(typeof(AbpValidationModule))]
public class EmployeeServiceDomainSharedModule : AbpModule
{
}
