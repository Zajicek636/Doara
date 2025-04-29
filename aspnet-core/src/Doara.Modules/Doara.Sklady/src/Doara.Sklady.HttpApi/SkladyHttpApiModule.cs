using Localization.Resources.AbpUi;
using Doara.Sklady.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Doara.Sklady;

[DependsOn(
    typeof(SkladyApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class SkladyHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(SkladyHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<SkladyResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
