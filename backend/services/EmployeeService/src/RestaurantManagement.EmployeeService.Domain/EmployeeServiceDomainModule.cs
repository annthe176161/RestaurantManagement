using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace RestaurantManagement.EmployeeService;

[DependsOn(typeof(AbpDddDomainModule), typeof(EmployeeServiceDomainSharedModule))]
public class EmployeeServiceDomainModule : AbpModule
{
}
