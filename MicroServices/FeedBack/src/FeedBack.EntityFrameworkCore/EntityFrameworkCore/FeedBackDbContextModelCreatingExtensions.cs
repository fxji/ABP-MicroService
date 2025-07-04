using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace FeedBack.EntityFrameworkCore;

public static class FeedBackDbContextModelCreatingExtensions
{
    public static void ConfigureFeedBack(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        /* Configure all entities here. Example:

        builder.Entity<Question>(b =>
        {
            //Configure table & schema name
            b.ToTable(FeedBackDbProperties.DbTablePrefix + "Questions", FeedBackDbProperties.DbSchema);

            b.ConfigureByConvention();

            //Properties
            b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);

            //Relations
            b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

            //Indexes
            b.HasIndex(q => q.CreationTime);
        });
        */
        builder.Entity<ProgramInfo>(b =>
        {
            b.ToTable(FeedBackDbProperties.DbTablePrefix + "ProgramInfo", FeedBackDbProperties.DbSchema);
            b.ConfigureByConvention();

            // b.HasMany(ProgramInfo=>ProgramInfo.ShapeInfos).WithOne().HasForeignKey(qt => qt.ProgramId);
        });

        builder.Entity<ShapeInfo>(b =>
        {
            b.ToTable(FeedBackDbProperties.DbTablePrefix + "ShapeInfo", FeedBackDbProperties.DbSchema);
            b.ConfigureByConvention();

        });
        
        builder.Entity<LineInfo>(b =>
        {
            b.ToTable(FeedBackDbProperties.DbTablePrefix + "LineInfo", FeedBackDbProperties.DbSchema);
            b.ConfigureByConvention();

        });
    }
}
