using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace FileStore.EntityFrameworkCore;

[DependsOn(
    typeof(FileStoreDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class FileStoreEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<FileStoreDbContext>(options =>
        {
            /* Add custom repositories here. Example:
             * options.AddRepository<Question, EfCoreQuestionRepository>();
             */
            Configure<AbpDbContextOptions>(options =>
            {
                options.UseSqlServer();
            });

            context.Services.AddAbpDbContext<FileStoreDbContext>(options =>
            {
                options.AddDefaultRepositories(includeAllEntities: true);
            });
        });
    }
}
