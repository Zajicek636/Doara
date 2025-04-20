using Doara.Ucetnictvi.EntityFrameworkCore;
using Doara.Ucetnictvi.EntityFrameworkCore.Base;
using Volo.Abp.Modularity;

namespace Doara.Ucetnictvi;

[DependsOn(
    typeof(UcetnictviApplicationModule),
    typeof(UcetnictviEntityFrameworkCoreTestModule),
    typeof(UcetnictviDomainTestModule),
    typeof(UcetnictviEntityFrameworkCoreModule)
    )]
public class UcetnictviApplicationTestModule : AbpModule
{

}
