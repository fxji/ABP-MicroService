using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace AAA;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class AAAInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AAAInstallerModule>();
        });
    }
}
