using Doara.Sklady;
using Volo.Abp.Modularity;

namespace Doara.Ucetnictvi;

[DependsOn(
    typeof(UcetnictviDomainModule),
    typeof(UcetnictviTestBaseModule),
    typeof(SkladyDomainModule)
)]
public class UcetnictviDomainTestModule : AbpModule
{

}
