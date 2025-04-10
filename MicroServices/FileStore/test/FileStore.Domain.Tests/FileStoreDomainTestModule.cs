using FileStore.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace FileStore;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(FileStoreEntityFrameworkCoreTestModule)
    )]
public class FileStoreDomainTestModule : AbpModule
{

}
