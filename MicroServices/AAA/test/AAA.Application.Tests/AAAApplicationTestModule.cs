using Volo.Abp.Modularity;

namespace AAA;

[DependsOn(
    typeof(AAAApplicationModule),
    typeof(AAADomainTestModule)
    )]
public class AAAApplicationTestModule : AbpModule
{

}
