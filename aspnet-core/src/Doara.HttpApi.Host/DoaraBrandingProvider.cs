using Microsoft.Extensions.Localization;
using Doara.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Doara;

[Dependency(ReplaceServices = true)]
public class DoaraBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<DoaraResource> _localizer;

    public DoaraBrandingProvider(IStringLocalizer<DoaraResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
