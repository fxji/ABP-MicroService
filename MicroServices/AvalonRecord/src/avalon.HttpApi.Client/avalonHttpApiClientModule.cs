using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace avalon;

[DependsOn(
    typeof(avalonApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class avalonHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(avalonApplicationContractsModule).Assembly,
            avalonRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<avalonHttpApiClientModule>();
        });

    }
}
