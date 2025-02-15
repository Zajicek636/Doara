using Volo.Abp.Modularity;

namespace Doara.Api;

public abstract class ApiApplicationTestBase<TStartupModule> : ApiTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
