using Localization.Resources.AbpUi;
using Doara.Ucetnictvi.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Doara.Ucetnictvi;

[DependsOn(
    typeof(UcetnictviApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class UcetnictviHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(UcetnictviHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<UcetnictviResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
