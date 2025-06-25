using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace avalon.MongoDB;

[ConnectionStringName(avalonDbProperties.ConnectionStringName)]
public interface IavalonMongoDbContext : IAbpMongoDbContext
{
    /* Define mongo collections here. Example:
     * IMongoCollection<Question> Questions { get; }
     */
}
