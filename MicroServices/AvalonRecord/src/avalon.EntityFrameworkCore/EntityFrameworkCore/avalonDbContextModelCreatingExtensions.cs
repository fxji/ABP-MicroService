using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace avalon.EntityFrameworkCore;

public static class avalonDbContextModelCreatingExtensions
{
    public static void Configureavalon(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        /* Configure all entities here. Example:

        builder.Entity<Question>(b =>
        {
            //Configure table & schema name
            b.ToTable(avalonDbProperties.DbTablePrefix + "Questions", avalonDbProperties.DbSchema);

            b.ConfigureByConvention();

            //Properties
            b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);

            //Relations
            b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

            //Indexes
            b.HasIndex(q => q.CreationTime);
        });
        */

        builder.Entity<GamePlayer>(b =>
        {
            b.ToTable(avalonDbProperties.DbTablePrefix + "GamePlayers", avalonDbProperties.DbSchema);
            b.ConfigureByConvention();
        });
        builder.Entity<GameResult>(b =>
        {
            b.ToTable(avalonDbProperties.DbTablePrefix + "GameResults", avalonDbProperties.DbSchema);
            b.ConfigureByConvention();
        });

        builder.Entity<Player>(b =>
        {
            b.ToTable(avalonDbProperties.DbTablePrefix + "Players", avalonDbProperties.DbSchema);
            b.ConfigureByConvention();
        });
    }
}
