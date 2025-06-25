using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace FeedBack.EntityFrameworkCore;

[ConnectionStringName(FeedBackDbProperties.ConnectionStringName)]
public class FeedBackDbContext : AbpDbContext<FeedBackDbContext>, IFeedBackDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

     public DbSet<ProgramInfo> ProgramInfos { get; set; }
     public DbSet<ShapeInfo> ShapeInfos { get; set; }
     public DbSet<FailedInfo> FailedInfos { get; set; }
     public DbSet<AoiInfo> AoiInfos { get; set; }
     public DbSet<AoiAction> AoiActions { get; set; }

    public FeedBackDbContext(DbContextOptions<FeedBackDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureFeedBack();
    }
}
