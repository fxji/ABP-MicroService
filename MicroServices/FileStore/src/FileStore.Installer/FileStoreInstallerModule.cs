﻿using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace FileStore;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class FileStoreInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<FileStoreInstallerModule>();
        });
    }
}
