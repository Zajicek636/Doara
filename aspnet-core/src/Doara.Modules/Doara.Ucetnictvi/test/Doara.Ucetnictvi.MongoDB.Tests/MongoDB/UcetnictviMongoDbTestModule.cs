using System;
using Volo.Abp.Data;
using Volo.Abp.Modularity;
using Volo.Abp.Uow;

namespace Doara.Ucetnictvi.MongoDB;

[DependsOn(
    typeof(UcetnictviApplicationTestModule),
    typeof(UcetnictviMongoDbModule)
)]
public class UcetnictviMongoDbTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = MongoDbFixture.GetRandomConnectionString();
        });
    }
}
