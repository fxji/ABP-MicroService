using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace FeedBack.MongoDB;

[ConnectionStringName(FeedBackDbProperties.ConnectionStringName)]
public class FeedBackMongoDbContext : AbpMongoDbContext, IFeedBackMongoDbContext
{
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureFeedBack();
    }
}
