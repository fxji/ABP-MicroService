using PeBusiness.Models;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace PeBusiness.EntityFrameworkCore
{
    [ConnectionStringName("PeBusiness")]
    public class PeBusinessDbContext : AbpDbContext<PeBusinessDbContext>
    {
        public DbSet<PeTask> PeTask { get; set; }

        //Code generation...

        public PeBusinessDbContext(DbContextOptions<PeBusinessDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ConfigurePeBusiness();
        }
    }
}
