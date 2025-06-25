using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace avalon;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class avalonInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<avalonInstallerModule>();
        });
    }
}
