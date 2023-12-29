using AAA.Models;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace AAA.EntityFrameworkCore;

public static class AAADbContextModelCreatingExtensions
{
    public static void ConfigureAAA(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        /* Configure all entities here. Example:

        builder.Entity<Question>(b =>
        {
            //Configure table & schema name
            b.ToTable(AAADbProperties.DbTablePrefix + "Questions", AAADbProperties.DbSchema);

            b.ConfigureByConvention();

            //Properties
            b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);

            //Relations
            b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

            //Indexes
            b.HasIndex(q => q.CreationTime);
        });
        */

        builder.Entity<A3>(b =>
        {
            b.ToTable("A3");

            b.ConfigureByConvention();

            b.Property(x => x.Name).IsRequired();
            b.HasIndex(x => x.Name);

        });

        builder.Entity<Issue>(b =>
        {
            b.ToTable("Issue");

            b.ConfigureByConvention();

            b.Property(x => x.Name).IsRequired();
            b.HasIndex(x => x.Name);

            b.Property(x => x.Type).IsRequired();
            b.HasIndex(x => x.Type);

        });


        

        builder.Entity<ContainmentAction>(b =>
        {
            b.ToTable("ContainmentAction");

            b.ConfigureByConvention();

            b.Property(x => x.Name).IsRequired();
            b.HasIndex(x => x.Name);

            b.Property(x => x.Type).IsRequired();
            b.HasIndex(x => x.Type);

        });

        builder.Entity<RiskAssessment>(b =>
        {
            b.ToTable("RiskAssessment");

            b.ConfigureByConvention();

            b.Property(x => x.Name).IsRequired();
            b.HasIndex(x => x.Name);
        });

        builder.Entity<Cause>(b =>
        {
            b.ToTable("Cause");

            b.ConfigureByConvention();

            b.Property(x => x.Name).IsRequired();
            b.HasIndex(x => x.Name);
        });

        builder.Entity<CorrectiveAction>(b =>
        {
            b.ToTable("CorrectiveAction");

            b.ConfigureByConvention();

            b.Property(x => x.Name).IsRequired();
            b.HasIndex(x => x.Name);
        });

        builder.Entity<A3Attachment>(b =>
        {
            b.ToTable("A3Attachment");

            b.ConfigureByConvention();

        });

        builder.Entity<A3Member>(b =>
        {
            b.ToTable("A3Member");

            b.ConfigureByConvention();

        });
    }
}
