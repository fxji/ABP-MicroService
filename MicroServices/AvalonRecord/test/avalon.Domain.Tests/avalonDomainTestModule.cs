using avalon.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace avalon;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(avalonEntityFrameworkCoreTestModule)
    )]
public class avalonDomainTestModule : AbpModule
{

}
