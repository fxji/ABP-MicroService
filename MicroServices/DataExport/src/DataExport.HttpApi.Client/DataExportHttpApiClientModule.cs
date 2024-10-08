using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace DataExport;

[DependsOn(
    typeof(DataExportApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class DataExportHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(DataExportApplicationContractsModule).Assembly,
            DataExportRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<DataExportHttpApiClientModule>();
        });

    }
}
