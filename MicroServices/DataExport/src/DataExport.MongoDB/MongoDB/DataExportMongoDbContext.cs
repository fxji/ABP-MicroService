using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace DataExport.MongoDB;

[ConnectionStringName(DataExportDbProperties.ConnectionStringName)]
public class DataExportMongoDbContext : AbpMongoDbContext, IDataExportMongoDbContext
{
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureDataExport();
    }
}
