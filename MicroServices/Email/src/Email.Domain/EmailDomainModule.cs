using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Email;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(EmailDomainSharedModule)
)]
public class EmailDomainModule : AbpModule
{

}
