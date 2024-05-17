using A3.ConfirmInfo.Models;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace A3.ConfirmInfo.EntityFrameworkCore
{
    [ConnectionStringName("A3.ConfirmInfo")]
    public class A3.ConfirmInfoDbContext : AbpDbContext<A3.ConfirmInfoDbContext>
    {
        public DbSet<ConfirmInfo> ConfirmInfo { get; set; }

        //Code generation...

        public A3.ConfirmInfoDbContext(DbContextOptions<A3.ConfirmInfoDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ConfigureA3.ConfirmInfo();
        }
    }
}
