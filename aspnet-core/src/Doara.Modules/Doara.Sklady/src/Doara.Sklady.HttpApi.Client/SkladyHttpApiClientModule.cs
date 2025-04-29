using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Doara.Sklady;

[DependsOn(
    typeof(SkladyApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class SkladyHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(SkladyApplicationContractsModule).Assembly,
            SkladyRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SkladyHttpApiClientModule>();
        });

    }
}
