using Doara.Api.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Doara.Api.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(ApiEntityFrameworkCoreModule),
    typeof(ApiApplicationContractsModule)
    )]
public class ApiDbMigratorModule : AbpModule
{
}
