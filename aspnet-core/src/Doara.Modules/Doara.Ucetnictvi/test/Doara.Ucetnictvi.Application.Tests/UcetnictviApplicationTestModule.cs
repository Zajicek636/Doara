using Volo.Abp.Modularity;

namespace Doara.Ucetnictvi;

[DependsOn(
    typeof(UcetnictviApplicationModule),
    typeof(UcetnictviDomainTestModule)
    )]
public class UcetnictviApplicationTestModule : AbpModule
{

}
