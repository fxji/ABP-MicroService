using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace PeBusiness.EntityFrameworkCore;

public class PeBusinessHttpApiHostMigrationsDbContext : AbpDbContext<PeBusinessHttpApiHostMigrationsDbContext>
{
    public PeBusinessHttpApiHostMigrationsDbContext(DbContextOptions<PeBusinessHttpApiHostMigrationsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigurePeBusiness();
    }
}
