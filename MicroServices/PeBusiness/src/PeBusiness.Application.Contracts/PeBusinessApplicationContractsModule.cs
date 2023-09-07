using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace PeBusiness;

[DependsOn(
    typeof(PeBusinessDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class PeBusinessApplicationContractsModule : AbpModule
{

}
