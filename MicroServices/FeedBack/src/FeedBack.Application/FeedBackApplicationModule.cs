using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;

namespace FeedBack;

[DependsOn(
    typeof(FeedBackDomainModule),
    typeof(FeedBackApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule)
    )]
public class FeedBackApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<FeedBackApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<FeedBackApplicationModule>(validate: false);
        });
    }
}
