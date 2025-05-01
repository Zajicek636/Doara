using Doara.Sklady.EntityFrameworkCore;
using Doara.Sklady.EntityFrameworkCore.Base;
using Volo.Abp.Modularity;

namespace Doara.Sklady;

[DependsOn(
    typeof(SkladyApplicationModule),
    typeof(SkladyDomainTestModule),
    typeof(SkladyEntityFrameworkCoreTestModule),
    typeof(SkladyEntityFrameworkCoreModule)
    )]
public class SkladyApplicationTestModule : AbpModule
{

}
