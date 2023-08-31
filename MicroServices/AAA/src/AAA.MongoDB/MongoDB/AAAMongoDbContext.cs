using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace AAA.MongoDB;

[ConnectionStringName(AAADbProperties.ConnectionStringName)]
public class AAAMongoDbContext : AbpMongoDbContext, IAAAMongoDbContext
{
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureAAA();
    }
}
