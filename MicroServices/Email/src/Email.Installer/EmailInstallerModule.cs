using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Email;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class EmailInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<EmailInstallerModule>();
        });
    }
}
