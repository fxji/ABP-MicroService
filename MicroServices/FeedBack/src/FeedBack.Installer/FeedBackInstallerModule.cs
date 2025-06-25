using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace FeedBack;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class FeedBackInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<FeedBackInstallerModule>();
        });
    }
}
