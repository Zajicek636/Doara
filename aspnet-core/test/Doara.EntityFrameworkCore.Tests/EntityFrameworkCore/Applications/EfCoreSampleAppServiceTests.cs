using Doara.Samples;
using Xunit;

namespace Doara.EntityFrameworkCore.Applications;

[Collection(DoaraTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<DoaraEntityFrameworkCoreTestModule>
{

}
