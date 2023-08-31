using AAA.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace AAA;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(AAAEntityFrameworkCoreTestModule)
    )]
public class AAADomainTestModule : AbpModule
{

}
