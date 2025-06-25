using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace FeedBack;

[DependsOn(
    typeof(FeedBackApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class FeedBackHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(FeedBackApplicationContractsModule).Assembly,
            FeedBackRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<FeedBackHttpApiClientModule>();
        });

    }
}
