using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace DataExport.MongoDB;

[ConnectionStringName(DataExportDbProperties.ConnectionStringName)]
public interface IDataExportMongoDbContext : IAbpMongoDbContext
{
    /* Define mongo collections here. Example:
     * IMongoCollection<Question> Questions { get; }
     */
}
