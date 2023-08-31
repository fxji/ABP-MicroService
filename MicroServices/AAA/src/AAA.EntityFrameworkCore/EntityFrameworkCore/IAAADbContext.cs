using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace AAA.EntityFrameworkCore;

[ConnectionStringName(AAADbProperties.ConnectionStringName)]
public interface IAAADbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
