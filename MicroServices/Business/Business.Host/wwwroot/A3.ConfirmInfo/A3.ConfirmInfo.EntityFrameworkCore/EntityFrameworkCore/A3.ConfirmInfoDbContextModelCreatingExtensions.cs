using A3.ConfirmInfo.Models;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace A3.ConfirmInfo.EntityFrameworkCore
{
    public static class A3.ConfirmInfoDbContextModelCreatingExtensions
    {
        public static void ConfigureA3.ConfirmInfo(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            builder.Entity<ConfirmInfo>(b =>
            {
                b.ToTable("A3.ConfirmInfo");

                b.ConfigureByConvention();
                
                b.Property(x => x.UserId).IsRequired();
                b.Property(x => x.A3Id).IsRequired();
                b.Property(x => x.Comments).IsRequired();
            });

            //Code generation...
        }
    }
}
