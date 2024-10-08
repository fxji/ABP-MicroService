using Volo.Abp;
using Volo.Abp.MongoDB;

namespace DataExport.MongoDB;

public static class DataExportMongoDbContextExtensions
{
    public static void ConfigureDataExport(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
