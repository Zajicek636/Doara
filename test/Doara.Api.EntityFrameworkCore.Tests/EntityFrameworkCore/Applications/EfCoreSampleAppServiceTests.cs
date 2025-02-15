using Doara.Api.Samples;
using Xunit;

namespace Doara.Api.EntityFrameworkCore.Applications;

[Collection(ApiTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<ApiEntityFrameworkCoreTestModule>
{

}
