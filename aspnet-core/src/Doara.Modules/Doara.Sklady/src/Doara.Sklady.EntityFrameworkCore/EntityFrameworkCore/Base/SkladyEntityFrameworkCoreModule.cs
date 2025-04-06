using Doara.Sklady.EntityFrameworkCore.Base;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Doara.Sklady.EntityFrameworkCore.Base;

[DependsOn(
    typeof(SkladyDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class SkladyEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<SkladyDbContext>(options =>
        {
            options.AddDefaultRepositories(includeAllEntities: true);
        });
    }
}
