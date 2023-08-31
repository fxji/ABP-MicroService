using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace AAA.EntityFrameworkCore;

public class AAAHttpApiHostMigrationsDbContext : AbpDbContext<AAAHttpApiHostMigrationsDbContext>
{
    public AAAHttpApiHostMigrationsDbContext(DbContextOptions<AAAHttpApiHostMigrationsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureAAA();
    }
}
