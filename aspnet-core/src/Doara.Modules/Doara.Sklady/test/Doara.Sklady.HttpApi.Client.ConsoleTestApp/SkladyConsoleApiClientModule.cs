using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Doara.Sklady;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(SkladyHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class SkladyConsoleApiClientModule : AbpModule
{

}
