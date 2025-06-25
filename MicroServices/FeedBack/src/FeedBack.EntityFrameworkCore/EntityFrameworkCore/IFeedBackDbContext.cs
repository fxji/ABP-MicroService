using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace FeedBack.EntityFrameworkCore;

[ConnectionStringName(FeedBackDbProperties.ConnectionStringName)]
public interface IFeedBackDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
