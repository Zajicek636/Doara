using Volo.Abp.Modularity;

namespace Doara;

/* Inherit from this class for your domain layer tests. */
public abstract class DoaraDomainTestBase<TStartupModule> : DoaraTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
