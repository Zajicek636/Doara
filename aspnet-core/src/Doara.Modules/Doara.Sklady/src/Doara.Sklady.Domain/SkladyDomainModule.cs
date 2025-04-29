using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Doara.Sklady;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(SkladyDomainSharedModule)
)]
public class SkladyDomainModule : AbpModule
{

}
