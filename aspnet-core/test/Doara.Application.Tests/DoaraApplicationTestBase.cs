using Volo.Abp.Modularity;

namespace Doara;

public abstract class DoaraApplicationTestBase<TStartupModule> : DoaraTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
