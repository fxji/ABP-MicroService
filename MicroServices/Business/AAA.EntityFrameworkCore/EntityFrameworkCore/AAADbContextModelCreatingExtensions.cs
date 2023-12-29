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

            builder.Entity<A3Member>(b =>
            {
                b.ToTable("A3Member");

                b.ConfigureByConvention();
                
                b.Property(x => x.UserId).IsRequired();
                b.Property(x => x.OrganizationId).IsRequired();
            });

            //Code generation...
        }
    }
}
