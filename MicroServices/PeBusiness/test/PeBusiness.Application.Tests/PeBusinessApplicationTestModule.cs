using Volo.Abp.Modularity;

namespace PeBusiness;

[DependsOn(
    typeof(PeBusinessApplicationModule),
    typeof(PeBusinessDomainTestModule)
    )]
public class PeBusinessApplicationTestModule : AbpModule
{

}
