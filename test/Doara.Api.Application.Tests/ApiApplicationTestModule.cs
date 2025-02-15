using Volo.Abp.Modularity;

namespace Doara.Api;

[DependsOn(
    typeof(ApiApplicationModule),
    typeof(ApiDomainTestModule)
)]
public class ApiApplicationTestModule : AbpModule
{

}
