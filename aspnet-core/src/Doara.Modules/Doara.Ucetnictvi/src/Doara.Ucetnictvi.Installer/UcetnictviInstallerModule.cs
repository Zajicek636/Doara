using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Doara.Ucetnictvi;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class UcetnictviInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<UcetnictviInstallerModule>();
        });
    }
}
