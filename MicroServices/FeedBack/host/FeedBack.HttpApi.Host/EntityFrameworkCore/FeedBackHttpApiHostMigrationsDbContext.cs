using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace FeedBack.EntityFrameworkCore;

public class FeedBackHttpApiHostMigrationsDbContext : AbpDbContext<FeedBackHttpApiHostMigrationsDbContext>
{
    public FeedBackHttpApiHostMigrationsDbContext(DbContextOptions<FeedBackHttpApiHostMigrationsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureFeedBack();
    }
}
