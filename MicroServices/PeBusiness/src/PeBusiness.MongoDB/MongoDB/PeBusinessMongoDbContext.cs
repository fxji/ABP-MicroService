using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace PeBusiness.MongoDB;

[ConnectionStringName(PeBusinessDbProperties.ConnectionStringName)]
public class PeBusinessMongoDbContext : AbpMongoDbContext, IPeBusinessMongoDbContext
{
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigurePeBusiness();
    }
}
