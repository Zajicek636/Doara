using Volo.Abp.Modularity;

namespace Doara.Ucetnictvi;

/* Inherit from this class for your domain layer tests.
 * See SampleManager_Tests for example.
 */
public abstract class UcetnictviDomainTestBase<TStartupModule> : UcetnictviTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
