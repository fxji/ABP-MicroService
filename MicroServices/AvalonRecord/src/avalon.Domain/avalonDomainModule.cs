using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace avalon;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(avalonDomainSharedModule)
)]
public class avalonDomainModule : AbpModule
{

}
