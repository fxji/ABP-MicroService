using Volo.Abp;
using Volo.Abp.MongoDB;

namespace AAA.MongoDB;

public static class AAAMongoDbContextExtensions
{
    public static void ConfigureAAA(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
