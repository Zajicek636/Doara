using Doara.Api.Samples;
using Xunit;

namespace Doara.Api.EntityFrameworkCore.Domains;

[Collection(ApiTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<ApiEntityFrameworkCoreTestModule>
{

}
