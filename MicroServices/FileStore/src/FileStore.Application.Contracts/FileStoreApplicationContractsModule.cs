using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace FileStore;

[DependsOn(
    typeof(FileStoreDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class FileStoreApplicationContractsModule : AbpModule
{

}
