using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace avalon.EntityFrameworkCore;

public class avalonHttpApiHostMigrationsDbContext : AbpDbContext<avalonHttpApiHostMigrationsDbContext>
{
    public avalonHttpApiHostMigrationsDbContext(DbContextOptions<avalonHttpApiHostMigrationsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Configureavalon();
    }
}
