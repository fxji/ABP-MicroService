using Volo.Abp;
using Volo.Abp.MongoDB;

namespace FeedBack.MongoDB;

public static class FeedBackMongoDbContextExtensions
{
    public static void ConfigureFeedBack(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
