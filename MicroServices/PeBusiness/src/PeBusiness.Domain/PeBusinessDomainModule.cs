using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace PeBusiness;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(PeBusinessDomainSharedModule)
)]
public class PeBusinessDomainModule : AbpModule
{

}
