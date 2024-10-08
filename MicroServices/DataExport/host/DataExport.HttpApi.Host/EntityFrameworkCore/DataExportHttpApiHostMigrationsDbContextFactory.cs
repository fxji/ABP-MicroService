using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DataExport.EntityFrameworkCore;

public class DataExportHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<DataExportHttpApiHostMigrationsDbContext>
{
    public DataExportHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<DataExportHttpApiHostMigrationsDbContext>()
            .UseSqlServer(configuration.GetConnectionString("DataExport"));

        return new DataExportHttpApiHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
