using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace DataExport.EntityFrameworkCore;

public class DataExportHttpApiHostMigrationsDbContext : AbpDbContext<DataExportHttpApiHostMigrationsDbContext>
{
    public DataExportHttpApiHostMigrationsDbContext(DbContextOptions<DataExportHttpApiHostMigrationsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureDataExport();
    }
}
