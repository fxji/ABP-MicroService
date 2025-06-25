using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace FeedBack.EntityFrameworkCore;

public class FeedBackHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<FeedBackHttpApiHostMigrationsDbContext>
{
    public FeedBackHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<FeedBackHttpApiHostMigrationsDbContext>()
            .UseSqlServer(configuration.GetConnectionString("FeedBack"));

        return new FeedBackHttpApiHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        //TODO: Implement the code for the TODO comment
        var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"appsettings.{environmentName}.json", optional: true);
        

        return builder.Build();
    }
}
