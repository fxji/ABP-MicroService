using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace DataExport.EntityFrameworkCore;

[ConnectionStringName(DataExportDbProperties.ConnectionStringName)]
public class DataExportDbContext : AbpDbContext<DataExportDbContext>, IDataExportDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public DataExportDbContext(DbContextOptions<DataExportDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureDataExport();
    }
}
