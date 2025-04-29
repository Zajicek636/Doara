using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace Doara.Ucetnictvi;

[DependsOn(
    typeof(UcetnictviDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class UcetnictviApplicationContractsModule : AbpModule
{

}
