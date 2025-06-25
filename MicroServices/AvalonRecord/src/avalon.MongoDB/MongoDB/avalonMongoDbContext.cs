using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace avalon.MongoDB;

[ConnectionStringName(avalonDbProperties.ConnectionStringName)]
public class avalonMongoDbContext : AbpMongoDbContext, IavalonMongoDbContext
{
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.Configureavalon();
    }
}
