using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace AAA.EntityFrameworkCore;

public class AAAHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<AAAHttpApiHostMigrationsDbContext>
{
    public AAAHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<AAAHttpApiHostMigrationsDbContext>()
            .UseSqlServer(configuration.GetConnectionString("AAA"));

        return new AAAHttpApiHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
