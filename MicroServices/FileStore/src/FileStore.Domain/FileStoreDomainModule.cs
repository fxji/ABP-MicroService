using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace FileStore;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(FileStoreDomainSharedModule)
)]
public class FileStoreDomainModule : AbpModule
{

}
