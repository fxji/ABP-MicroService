using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace AAA;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(AAADomainSharedModule)
)]
public class AAADomainModule : AbpModule
{

}
