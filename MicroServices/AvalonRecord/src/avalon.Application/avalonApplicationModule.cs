using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;

namespace avalon;

[DependsOn(
    typeof(avalonDomainModule),
    typeof(avalonApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule)
    )]
public class avalonApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<avalonApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<avalonApplicationModule>(validate: false);
        });
    }
}
