using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace FileStore.EntityFrameworkCore;

public class FileStoreHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<FileStoreHttpApiHostMigrationsDbContext>
{
    public FileStoreHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<FileStoreHttpApiHostMigrationsDbContext>()
            .UseSqlServer(configuration.GetConnectionString("FileStore"));

        return new FileStoreHttpApiHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
