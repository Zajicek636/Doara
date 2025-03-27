using Volo.Abp.Modularity;

namespace Doara;

[DependsOn(
    typeof(DoaraDomainModule),
    typeof(DoaraTestBaseModule)
)]
public class DoaraDomainTestModule : AbpModule
{

}
