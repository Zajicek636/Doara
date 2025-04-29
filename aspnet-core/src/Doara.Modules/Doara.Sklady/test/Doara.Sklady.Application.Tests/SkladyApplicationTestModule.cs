using Volo.Abp.Modularity;

namespace Doara.Sklady;

[DependsOn(
    typeof(SkladyApplicationModule),
    typeof(SkladyDomainTestModule)
    )]
public class SkladyApplicationTestModule : AbpModule
{

}
