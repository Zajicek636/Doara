using Volo.Abp.Bundling;

namespace Sklady.Blazor.Host.Client;

public class SkladyBlazorHostBundleContributor : IBundleContributor
{
    public void AddScripts(BundleContext context)
    {

    }

    public void AddStyles(BundleContext context)
    {
        context.Add("main.css", true);
    }
}
