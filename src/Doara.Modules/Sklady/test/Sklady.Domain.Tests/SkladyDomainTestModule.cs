using Volo.Abp.Modularity;

namespace Sklady;

[DependsOn(
    typeof(SkladyDomainModule),
    typeof(SkladyTestBaseModule)
)]
public class SkladyDomainTestModule : AbpModule
{

}
