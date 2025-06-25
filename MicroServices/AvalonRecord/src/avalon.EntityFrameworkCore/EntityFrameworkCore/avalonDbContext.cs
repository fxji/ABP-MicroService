using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace avalon.EntityFrameworkCore;

[ConnectionStringName(avalonDbProperties.ConnectionStringName)]
public class avalonDbContext : AbpDbContext<avalonDbContext>, IavalonDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public DbSet<Player> Players { get; set; }
    public DbSet<GameResult> GameResults { get; set; }

    public avalonDbContext(DbContextOptions<avalonDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Configureavalon();
    }
}
