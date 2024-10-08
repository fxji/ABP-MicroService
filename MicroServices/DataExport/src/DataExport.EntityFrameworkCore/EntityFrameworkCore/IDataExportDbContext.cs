using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace DataExport.EntityFrameworkCore;

[ConnectionStringName(DataExportDbProperties.ConnectionStringName)]
public interface IDataExportDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
