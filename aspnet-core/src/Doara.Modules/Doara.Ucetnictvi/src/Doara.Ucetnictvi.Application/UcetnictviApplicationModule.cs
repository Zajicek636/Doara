using Doara.Sklady;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;

namespace Doara.Ucetnictvi;

[DependsOn(
    typeof(UcetnictviDomainModule),
    typeof(UcetnictviApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule),
    typeof(SkladyApplicationContractsModule)
    )]
public class UcetnictviApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<UcetnictviApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<UcetnictviApplicationModule>(validate: true);
        });
    }
}
