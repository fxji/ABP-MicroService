using Volo.Abp;
using Volo.Abp.MongoDB;

namespace FileStore.MongoDB;

public static class FileStoreMongoDbContextExtensions
{
    public static void ConfigureFileStore(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
