using Volo.Abp;
using Volo.Abp.MongoDB;

namespace PeBusiness.MongoDB;

public static class PeBusinessMongoDbContextExtensions
{
    public static void ConfigurePeBusiness(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
