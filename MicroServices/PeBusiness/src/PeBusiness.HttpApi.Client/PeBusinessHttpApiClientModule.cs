using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace PeBusiness;

[DependsOn(
    typeof(PeBusinessApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class PeBusinessHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(PeBusinessApplicationContractsModule).Assembly,
            PeBusinessRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<PeBusinessHttpApiClientModule>();
        });

    }
}
