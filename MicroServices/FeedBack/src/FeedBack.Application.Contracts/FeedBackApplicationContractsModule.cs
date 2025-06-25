using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace FeedBack;

[DependsOn(
    typeof(FeedBackDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class FeedBackApplicationContractsModule : AbpModule
{

}
