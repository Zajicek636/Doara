using Xunit;

namespace Doara.Api.EntityFrameworkCore;

[CollectionDefinition(ApiTestConsts.CollectionDefinitionName)]
public class ApiEntityFrameworkCoreCollection : ICollectionFixture<ApiEntityFrameworkCoreFixture>
{

}
