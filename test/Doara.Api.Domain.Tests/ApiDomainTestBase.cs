using Volo.Abp.Modularity;

namespace Doara.Api;

/* Inherit from this class for your domain layer tests. */
public abstract class ApiDomainTestBase<TStartupModule> : ApiTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
