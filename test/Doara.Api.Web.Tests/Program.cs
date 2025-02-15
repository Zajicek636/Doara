using Microsoft.AspNetCore.Builder;
using Doara.Api;
using Volo.Abp.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();

builder.Environment.ContentRootPath = GetWebProjectContentRootPathHelper.Get("Doara.Api.Web.csproj");
await builder.RunAbpModuleAsync<ApiWebTestModule>(applicationName: "Doara.Api.Web" );

public partial class Program
{
}
