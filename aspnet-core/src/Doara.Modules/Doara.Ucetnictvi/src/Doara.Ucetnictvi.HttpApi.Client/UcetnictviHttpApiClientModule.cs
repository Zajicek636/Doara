using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Doara.Ucetnictvi;

[DependsOn(
    typeof(UcetnictviApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class UcetnictviHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(UcetnictviApplicationContractsModule).Assembly,
            UcetnictviRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<UcetnictviHttpApiClientModule>();
        });

    }
}
