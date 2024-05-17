using AAA.Models;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace AAA.EntityFrameworkCore;

[ConnectionStringName(AAADbProperties.ConnectionStringName)]
public class AAADbContext : AbpDbContext<AAADbContext>, IAAADbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */
    public DbSet<A3> A3s { get; set; }

    public DbSet<Issue> Issues { get; set; }
    
    public DbSet<ContainmentAction> ContainmentActions { get; set; }

    public DbSet<RiskAssessment> RiskAssessments { get; set; }

    public DbSet<Cause> Causes { get; set; }

    public DbSet<CorrectiveAction> CorrectiveActions { get; set; }

    public DbSet<A3Attachment> A3Attachments { get; set; }

    public DbSet<A3Member> A3Members { get; set; }

    public DbSet<ConfirmInfo> ConfirmInfos { get; set; }


    public AAADbContext(DbContextOptions<AAADbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureAAA();
    }
}
