using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace FileStore.EntityFrameworkCore;

public class FileStoreHttpApiHostMigrationsDbContext : AbpDbContext<FileStoreHttpApiHostMigrationsDbContext>
{
    public FileStoreHttpApiHostMigrationsDbContext(DbContextOptions<FileStoreHttpApiHostMigrationsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureFileStore();
    }
}
