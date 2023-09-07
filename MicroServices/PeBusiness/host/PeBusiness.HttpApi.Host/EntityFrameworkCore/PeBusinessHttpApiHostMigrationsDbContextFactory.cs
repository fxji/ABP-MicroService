using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace PeBusiness.EntityFrameworkCore;

public class PeBusinessHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<PeBusinessHttpApiHostMigrationsDbContext>
{
    public PeBusinessHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<PeBusinessHttpApiHostMigrationsDbContext>()
            .UseSqlServer(configuration.GetConnectionString("PeBusiness"));

        return new PeBusinessHttpApiHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
