using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Doara.Api.Pages;

public class Index_Tests : ApiWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
