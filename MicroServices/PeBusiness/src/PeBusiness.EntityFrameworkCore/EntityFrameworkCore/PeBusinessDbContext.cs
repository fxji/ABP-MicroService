using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace PeBusiness.EntityFrameworkCore;

[ConnectionStringName(PeBusinessDbProperties.ConnectionStringName)]
public class PeBusinessDbContext : AbpDbContext<PeBusinessDbContext>, IPeBusinessDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public PeBusinessDbContext(DbContextOptions<PeBusinessDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigurePeBusiness();
    }
}
