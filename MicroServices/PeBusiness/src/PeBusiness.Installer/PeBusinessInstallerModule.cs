using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace PeBusiness;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class PeBusinessInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<PeBusinessInstallerModule>();
        });
    }
}
