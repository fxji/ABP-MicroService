using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace AAA;

[DependsOn(
    typeof(AAAApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class AAAHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(AAAApplicationContractsModule).Assembly,
            AAARemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AAAHttpApiClientModule>();
        });

    }
}
