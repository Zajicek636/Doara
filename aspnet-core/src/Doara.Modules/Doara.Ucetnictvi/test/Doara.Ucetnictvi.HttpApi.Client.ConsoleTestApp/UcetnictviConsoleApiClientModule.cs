using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Doara.Ucetnictvi;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(UcetnictviHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class UcetnictviConsoleApiClientModule : AbpModule
{

}
