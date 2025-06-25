using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace avalon;

[DependsOn(
    typeof(avalonDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class avalonApplicationContractsModule : AbpModule
{

}
