using Volo.Abp.Modularity;

namespace FeedBack;

[DependsOn(
    typeof(FeedBackApplicationModule),
    typeof(FeedBackDomainTestModule)
    )]
public class FeedBackApplicationTestModule : AbpModule
{

}
