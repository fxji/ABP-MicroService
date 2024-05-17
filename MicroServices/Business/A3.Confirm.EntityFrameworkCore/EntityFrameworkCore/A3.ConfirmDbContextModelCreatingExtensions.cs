using A3.Confirm.Models;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace A3.Confirm.EntityFrameworkCore
{
    public static class A3.ConfirmDbContextModelCreatingExtensions
    {
        public static void ConfigureA3.Confirm(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            builder.Entity<Confirm>(b =>
            {
                b.ToTable("A3.Confirm");

                b.ConfigureByConvention();
                
                b.Property(x => x.A3Id).IsRequired();
                b.Property(x => x.UserId).IsRequired();
                b.Property(x => x.Comments).IsRequired();
            });

            builder.Entity<Confirm>(b =>
            {
                b.ToTable("A3.Confirm");

                b.ConfigureByConvention();
                
                b.Property(x => x.UserId).IsRequired();
                b.Property(x => x.A3Id).IsRequired();
                b.Property(x => x.Comments).IsRequired();
            });

            //Code generation...
        }
    }
}
