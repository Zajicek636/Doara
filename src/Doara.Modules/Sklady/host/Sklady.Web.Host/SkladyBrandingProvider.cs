using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Sklady;

[Dependency(ReplaceServices = true)]
public class SkladyBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Sklady";
}
