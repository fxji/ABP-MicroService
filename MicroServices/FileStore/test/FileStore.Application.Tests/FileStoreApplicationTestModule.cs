using Volo.Abp.Modularity;

namespace FileStore;

[DependsOn(
    typeof(FileStoreApplicationModule),
    typeof(FileStoreDomainTestModule)
    )]
public class FileStoreApplicationTestModule : AbpModule
{

}
