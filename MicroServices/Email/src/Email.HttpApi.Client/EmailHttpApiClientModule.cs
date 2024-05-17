using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Email;

[DependsOn(
    typeof(EmailApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class EmailHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(EmailApplicationContractsModule).Assembly,
            EmailRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<EmailHttpApiClientModule>();
        });

    }
}
