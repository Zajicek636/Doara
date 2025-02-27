using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Sklady;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(SkladyHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class SkladyConsoleApiClientModule : AbpModule
{

}
