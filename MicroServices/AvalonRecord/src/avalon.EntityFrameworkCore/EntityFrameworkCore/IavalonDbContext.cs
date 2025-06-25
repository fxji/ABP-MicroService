using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace avalon.EntityFrameworkCore;

[ConnectionStringName(avalonDbProperties.ConnectionStringName)]
public interface IavalonDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
