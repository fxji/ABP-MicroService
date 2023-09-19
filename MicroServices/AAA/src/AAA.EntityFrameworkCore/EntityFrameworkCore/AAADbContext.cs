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

    public DbSet<Product> Products { get; set; }

    public DbSet<Project> Projects { get; set; }

    public DbSet<Part> Parts { get; set; }

    public DbSet<Customer> Customers { get; set; }

    public DbSet<ContainmentAction> ContainmentActions { get; set; }

    public DbSet<RiskAssessment> RiskAssessments { get; set; }

    public DbSet<Cause> Causes { get; set; }

    public DbSet<CorrectiveAction> CorrectiveActions { get; set; }

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
