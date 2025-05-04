using Doara.Sklady.EntityFrameworkCore.Base;
using Doara.Ucetnictvi.EntityFrameworkCore;
using Doara.Ucetnictvi.EntityFrameworkCore.Base;
using Volo.Abp.Modularity;

namespace Doara.Ucetnictvi;

[DependsOn(
    typeof(UcetnictviApplicationModule),
    typeof(UcetnictviEntityFrameworkCoreTestModule),
    typeof(UcetnictviDomainTestModule),
    typeof(UcetnictviEntityFrameworkCoreModule),
    typeof(SkladyEntityFrameworkCoreModule)
    )]
public class UcetnictviApplicationTestModule : AbpModule
{

}
