using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using DataExport.Settings;

namespace DataExport;

[DependsOn(
    typeof(DataExportDomainModule),
    typeof(DataExportApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule)
    )]
public class DataExportApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        Configure<DataExportOptions>(configuration.GetSection("exportSetting"));

        context.Services.AddAutoMapperObjectMapper<DataExportApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<DataExportApplicationModule>(validate: true);
        });
    }
}
