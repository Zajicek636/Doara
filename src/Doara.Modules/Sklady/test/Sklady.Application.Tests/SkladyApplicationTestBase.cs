using Volo.Abp.Modularity;

namespace Sklady;

/* Inherit from this class for your application layer tests.
 * See SampleAppService_Tests for example.
 */
public abstract class SkladyApplicationTestBase<TStartupModule> : SkladyTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
