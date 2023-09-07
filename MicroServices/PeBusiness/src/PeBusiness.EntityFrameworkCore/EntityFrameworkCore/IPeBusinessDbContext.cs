using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace PeBusiness.EntityFrameworkCore;

[ConnectionStringName(PeBusinessDbProperties.ConnectionStringName)]
public interface IPeBusinessDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
