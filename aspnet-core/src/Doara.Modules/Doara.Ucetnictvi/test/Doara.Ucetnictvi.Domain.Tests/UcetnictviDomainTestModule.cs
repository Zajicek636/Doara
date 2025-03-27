using Volo.Abp.Modularity;

namespace Doara.Ucetnictvi;

[DependsOn(
    typeof(UcetnictviDomainModule),
    typeof(UcetnictviTestBaseModule)
)]
public class UcetnictviDomainTestModule : AbpModule
{

}
