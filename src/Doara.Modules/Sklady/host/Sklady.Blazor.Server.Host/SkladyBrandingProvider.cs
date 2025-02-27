using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Sklady.Blazor.Server.Host;

[Dependency(ReplaceServices = true)]
public class SkladyBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Sklady";
}
