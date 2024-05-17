using Volo.Abp;
using Volo.Abp.MongoDB;

namespace Email.MongoDB;

public static class EmailMongoDbContextExtensions
{
    public static void ConfigureEmail(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
