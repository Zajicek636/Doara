using Volo.Abp.Modularity;

namespace Sklady;

[DependsOn(
    typeof(SkladyApplicationModule),
    typeof(SkladyDomainTestModule)
    )]
public class SkladyApplicationTestModule : AbpModule
{

}
