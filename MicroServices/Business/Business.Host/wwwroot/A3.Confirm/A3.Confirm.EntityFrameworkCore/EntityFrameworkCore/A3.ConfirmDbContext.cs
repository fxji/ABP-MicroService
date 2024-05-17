using A3.Confirm.Models;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace A3.Confirm.EntityFrameworkCore
{
    [ConnectionStringName("A3.Confirm")]
    public class A3.ConfirmDbContext : AbpDbContext<A3.ConfirmDbContext>
    {
        public DbSet<Confirm> Confirm { get; set; }

        public DbSet<Confirm> Confirm { get; set; }

        //Code generation...

        public A3.ConfirmDbContext(DbContextOptions<A3.ConfirmDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ConfigureA3.Confirm();
        }
    }
}
