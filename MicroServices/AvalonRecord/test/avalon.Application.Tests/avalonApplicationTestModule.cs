using Volo.Abp.Modularity;

namespace avalon;

[DependsOn(
    typeof(avalonApplicationModule),
    typeof(avalonDomainTestModule)
    )]
public class avalonApplicationTestModule : AbpModule
{

}
