﻿using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace Doara.Sklady;

[DependsOn(
    typeof(SkladyDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class SkladyApplicationContractsModule : AbpModule
{

}
