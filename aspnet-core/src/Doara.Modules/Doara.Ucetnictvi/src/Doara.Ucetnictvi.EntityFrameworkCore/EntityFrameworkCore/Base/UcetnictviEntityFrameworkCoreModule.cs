using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Doara.Ucetnictvi.EntityFrameworkCore.Base;

[DependsOn(
    typeof(UcetnictviDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class UcetnictviEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<UcetnictviDbContext>(options =>
        {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */
        });
    }
}
