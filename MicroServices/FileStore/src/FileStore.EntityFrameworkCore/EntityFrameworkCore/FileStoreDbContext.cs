using FileStore.Models;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace FileStore.EntityFrameworkCore;

[ConnectionStringName(FileStoreDbProperties.ConnectionStringName)]
public class FileStoreDbContext : AbpDbContext<FileStoreDbContext>, IFileStoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */


    public DbSet<FileDetail> FileDetails { get; set; }

    public FileStoreDbContext(DbContextOptions<FileStoreDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureFileStore();
    }
}
