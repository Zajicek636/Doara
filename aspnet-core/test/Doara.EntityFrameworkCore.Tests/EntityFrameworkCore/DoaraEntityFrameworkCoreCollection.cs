using Xunit;

namespace Doara.EntityFrameworkCore;

[CollectionDefinition(DoaraTestConsts.CollectionDefinitionName)]
public class DoaraEntityFrameworkCoreCollection : ICollectionFixture<DoaraEntityFrameworkCoreFixture>
{

}
