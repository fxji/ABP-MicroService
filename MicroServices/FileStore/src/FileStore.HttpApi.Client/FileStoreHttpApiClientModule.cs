using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace FileStore;

[DependsOn(
    typeof(FileStoreApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class FileStoreHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(FileStoreApplicationContractsModule).Assembly,
            FileStoreRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<FileStoreHttpApiClientModule>();
        });

    }
}
