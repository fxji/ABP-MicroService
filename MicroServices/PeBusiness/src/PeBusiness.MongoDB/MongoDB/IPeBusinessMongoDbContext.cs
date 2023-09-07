using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace PeBusiness.MongoDB;

[ConnectionStringName(PeBusinessDbProperties.ConnectionStringName)]
public interface IPeBusinessMongoDbContext : IAbpMongoDbContext
{
    /* Define mongo collections here. Example:
     * IMongoCollection<Question> Questions { get; }
     */
}
