using AAA.Models;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace AAA.EntityFrameworkCore
{
    [ConnectionStringName("AAA")]
    public class AAADbContext : AbpDbContext<AAADbContext>
    {
        public DbSet<A3> A3 { get; set; }

        public DbSet<A3> A3 { get; set; }

        //Code generation...

        public AAADbContext(DbContextOptions<AAADbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ConfigureAAA();
        }
    }
}
