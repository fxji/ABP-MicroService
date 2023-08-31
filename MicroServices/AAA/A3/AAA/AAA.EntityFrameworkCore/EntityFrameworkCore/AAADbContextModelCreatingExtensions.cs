using AAA.Models;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace AAA.EntityFrameworkCore
{
    public static class AAADbContextModelCreatingExtensions
    {
        public static void ConfigureAAA(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            builder.Entity<A3>(b =>
            {
                b.ToTable("A3");

                b.ConfigureByConvention();
                
                b.Property(x => x.Name).IsRequired();
                b.HasIndex(x => x.Name);

            });

            //Code generation...
        }
    }
}
