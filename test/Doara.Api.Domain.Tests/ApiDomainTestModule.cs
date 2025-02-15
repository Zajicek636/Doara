using Volo.Abp.Modularity;

namespace Doara.Api;

[DependsOn(
    typeof(ApiDomainModule),
    typeof(ApiTestBaseModule)
)]
public class ApiDomainTestModule : AbpModule
{

}
