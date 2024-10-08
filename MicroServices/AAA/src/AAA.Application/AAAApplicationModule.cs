using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using Email;
using DataExport;
using BaseService.HttpApi.Client;
using BaseService;
using AAA.A3Management;

namespace AAA;

[DependsOn(
    typeof(AAADomainModule),
    typeof(AAAApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule),
    typeof(EmailApplicationModule),
    typeof(DataExportApplicationModule),
    typeof(BaseServiceHttpApiClientModule),
    typeof(BaseServiceApplicationContractsModule)
    )]
public class AAAApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {

        var configuration = context.Services.GetConfiguration();
        Configure<ServerConfigurationOptions>(configuration.GetSection("ServerConfigre"));

        context.Services.AddAutoMapperObjectMapper<AAAApplicationModule>();

        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<AAAApplicationModule>(validate: false);
        });
    }
}
