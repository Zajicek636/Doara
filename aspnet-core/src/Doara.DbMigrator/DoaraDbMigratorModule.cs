using Doara.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Doara.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(DoaraEntityFrameworkCoreModule),
    typeof(DoaraApplicationContractsModule)
    )]
public class DoaraDbMigratorModule : AbpModule
{
}
