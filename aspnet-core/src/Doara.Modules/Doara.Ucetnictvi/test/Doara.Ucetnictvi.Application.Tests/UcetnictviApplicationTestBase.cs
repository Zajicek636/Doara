using Volo.Abp.Modularity;

namespace Doara.Ucetnictvi;

/* Inherit from this class for your application layer tests.
 * See SampleAppService_Tests for example.
 */
public abstract class UcetnictviApplicationTestBase<TStartupModule> : UcetnictviTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
