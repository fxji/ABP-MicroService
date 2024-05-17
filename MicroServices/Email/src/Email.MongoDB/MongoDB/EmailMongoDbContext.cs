using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Email.MongoDB;

[ConnectionStringName(EmailDbProperties.ConnectionStringName)]
public class EmailMongoDbContext : AbpMongoDbContext, IEmailMongoDbContext
{
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureEmail();
    }
}
