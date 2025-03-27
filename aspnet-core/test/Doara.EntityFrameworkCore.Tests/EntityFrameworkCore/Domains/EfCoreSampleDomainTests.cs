using Doara.Samples;
using Xunit;

namespace Doara.EntityFrameworkCore.Domains;

[Collection(DoaraTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<DoaraEntityFrameworkCoreTestModule>
{

}
