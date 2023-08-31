using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace AAA.MongoDB;

[ConnectionStringName(AAADbProperties.ConnectionStringName)]
public interface IAAAMongoDbContext : IAbpMongoDbContext
{
    /* Define mongo collections here. Example:
     * IMongoCollection<Question> Questions { get; }
     */
}
