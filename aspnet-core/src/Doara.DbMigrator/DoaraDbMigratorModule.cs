using Doara.EntityFrameworkCore;
using Doara.Sklady;
using Doara.Sklady.EntityFrameworkCore.Base;
using Doara.Ucetnictvi;
using Doara.Ucetnictvi.EntityFrameworkCore.Base;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Doara.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(DoaraEntityFrameworkCoreModule),
    typeof(DoaraApplicationContractsModule),
    typeof(SkladyEntityFrameworkCoreModule),
    typeof(SkladyApplicationContractsModule),
    typeof(UcetnictviEntityFrameworkCoreModule),
    typeof(UcetnictviApplicationContractsModule)
    )]
public class DoaraDbMigratorModule : AbpModule
{
}
