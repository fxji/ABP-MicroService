using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace DataExport;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class DataExportInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<DataExportInstallerModule>();
        });
    }
}
