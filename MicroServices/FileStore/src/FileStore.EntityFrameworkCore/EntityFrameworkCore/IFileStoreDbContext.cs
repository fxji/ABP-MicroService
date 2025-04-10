using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace FileStore.EntityFrameworkCore;

[ConnectionStringName(FileStoreDbProperties.ConnectionStringName)]
public interface IFileStoreDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
