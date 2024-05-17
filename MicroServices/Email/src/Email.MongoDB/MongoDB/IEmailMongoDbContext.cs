using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Email.MongoDB;

[ConnectionStringName(EmailDbProperties.ConnectionStringName)]
public interface IEmailMongoDbContext : IAbpMongoDbContext
{
    /* Define mongo collections here. Example:
     * IMongoCollection<Question> Questions { get; }
     */
}
