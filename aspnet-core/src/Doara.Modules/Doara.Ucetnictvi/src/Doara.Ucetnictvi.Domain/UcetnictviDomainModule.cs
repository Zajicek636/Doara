using Doara.Sklady;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Doara.Ucetnictvi;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(UcetnictviDomainSharedModule),
    typeof(SkladyDomainSharedModule)
)]
public class UcetnictviDomainModule : AbpModule
{

}
