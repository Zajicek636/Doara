using Volo.Abp.Modularity;

namespace Doara.Sklady;

[DependsOn(
    typeof(SkladyDomainModule),
    typeof(SkladyTestBaseModule)
)]
public class SkladyDomainTestModule : AbpModule
{

}
