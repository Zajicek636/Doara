using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Doara.Ucetnictvi;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(UcetnictviDomainSharedModule)
)]
public class UcetnictviDomainModule : AbpModule
{

}
