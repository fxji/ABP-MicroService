using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace FeedBack.MongoDB;

[ConnectionStringName(FeedBackDbProperties.ConnectionStringName)]
public interface IFeedBackMongoDbContext : IAbpMongoDbContext
{
    /* Define mongo collections here. Example:
     * IMongoCollection<Question> Questions { get; }
     */
}
