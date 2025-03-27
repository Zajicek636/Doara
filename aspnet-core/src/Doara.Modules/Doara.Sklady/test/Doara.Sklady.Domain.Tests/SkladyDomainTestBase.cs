using Volo.Abp.Modularity;

namespace Doara.Sklady;

/* Inherit from this class for your domain layer tests.
 * See SampleManager_Tests for example.
 */
public abstract class SkladyDomainTestBase<TStartupModule> : SkladyTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
