using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace FeedBack;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(FeedBackDomainSharedModule)
)]
public class FeedBackDomainModule : AbpModule
{

}
