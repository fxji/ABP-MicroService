using FeedBack.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace FeedBack;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(FeedBackEntityFrameworkCoreTestModule)
    )]
public class FeedBackDomainTestModule : AbpModule
{

}
