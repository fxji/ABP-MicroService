using FileStore.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace FileStore.EntityFrameworkCore;

public static class FileStoreDbContextModelCreatingExtensions
{
    public static void ConfigureFileStore(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        /* Configure all entities here. Example:

        builder.Entity<Question>(b =>
        {
            //Configure table & schema name
            b.ToTable(FileStoreDbProperties.DbTablePrefix + "Questions", FileStoreDbProperties.DbSchema);

            b.ConfigureByConvention();

            //Properties
            b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);

            //Relations
            b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

            //Indexes
            b.HasIndex(q => q.CreationTime);
        });
        */

        builder.Entity<FileDetail>(b =>
        {
            b.ToTable("file_info");

            b.ConfigureByConvention();

            b.Property(x => x.Name).IsRequired().HasMaxLength(64);
            b.Property(x => x.RealName).IsRequired().HasMaxLength(64);
            b.Property(x => x.Size).IsRequired().HasMaxLength(100);
            b.Property(x => x.Suffix).IsRequired().HasMaxLength(50);
            b.Property(x => x.Md5Code).IsRequired().HasMaxLength(256);
            b.Property(x => x.Path).IsRequired().HasMaxLength(256);
            b.Property(x => x.Url).IsRequired().HasMaxLength(256);
        });
    }
}
