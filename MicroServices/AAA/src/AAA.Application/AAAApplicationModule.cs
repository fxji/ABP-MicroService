using AAA.A3Management;
using BaseService;
using BaseService.HttpApi.Client;
using DataExport;
using Email;
using FileStore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace AAA;

[DependsOn(
    typeof(AAADomainModule),
    typeof(AAAApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule),

    typeof(EmailApplicationModule),
    typeof(EmailApplicationContractsModule),

    typeof(DataExportApplicationModule),
    typeof(DataExportApplicationContractsModule),

    typeof(BaseServiceHttpApiClientModule),
    //typeof(BaseServiceApplicationContractsModule)    ,
    typeof(FileStoreHttpApiClientModule)
    ,
    typeof(FileStoreApplicationContractsModule)
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
