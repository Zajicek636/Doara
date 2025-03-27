using Volo.Abp.Modularity;

namespace Doara;

[DependsOn(
    typeof(DoaraApplicationModule),
    typeof(DoaraDomainTestModule)
)]
public class DoaraApplicationTestModule : AbpModule
{

}
