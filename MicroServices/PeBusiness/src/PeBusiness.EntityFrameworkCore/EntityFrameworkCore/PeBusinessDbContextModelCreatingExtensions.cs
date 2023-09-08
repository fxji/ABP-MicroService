using PeBusiness.Models;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace PeBusiness.EntityFrameworkCore
{
    public static class PeBusinessDbContextModelCreatingExtensions
    {
        public static void ConfigurePeBusiness(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            builder.Entity<PeTask>(b =>
            {
                b.ToTable("PeTask");

                b.ConfigureByConvention();
                
                b.Property(x => x.Name).IsRequired();
            });

            //Code generation...
        }
    }
}
