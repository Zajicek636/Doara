using Microsoft.Extensions.Localization;
using Doara.Api.Localization;
using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Doara.Api.Web;

[Dependency(ReplaceServices = true)]
public class ApiBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<ApiResource> _localizer;

    public ApiBrandingProvider(IStringLocalizer<ApiResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
