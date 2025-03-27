using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Doara.Sklady;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class SkladyInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SkladyInstallerModule>();
        });
    }
}
