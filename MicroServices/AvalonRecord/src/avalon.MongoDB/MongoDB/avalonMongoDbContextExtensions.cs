using Volo.Abp;
using Volo.Abp.MongoDB;

namespace avalon.MongoDB;

public static class avalonMongoDbContextExtensions
{
    public static void Configureavalon(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
