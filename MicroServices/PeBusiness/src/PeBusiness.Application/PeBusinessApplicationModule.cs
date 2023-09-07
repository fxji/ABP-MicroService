using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;

namespace PeBusiness;

[DependsOn(
    typeof(PeBusinessDomainModule),
    typeof(PeBusinessApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule)
    )]
public class PeBusinessApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<PeBusinessApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<PeBusinessApplicationModule>(validate: true);
        });
    }
}
