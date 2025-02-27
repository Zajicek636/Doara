using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Sklady;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(SkladyDomainSharedModule)
)]
public class SkladyDomainModule : AbpModule
{

}
