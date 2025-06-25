using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace avalon.EntityFrameworkCore;

public class avalonHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<avalonHttpApiHostMigrationsDbContext>
{
    public avalonHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<avalonHttpApiHostMigrationsDbContext>()
            .UseSqlServer(configuration.GetConnectionString("avalon"));

        return new avalonHttpApiHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var evenvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
        
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"appsettings.{evenvironment}.json", optional: false);

        return builder.Build();
    }
}
