using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace avalon.EntityFrameworkCore;

[DependsOn(
    typeof(avalonDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class avalonEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<avalonDbContext>(options =>
        {
            /* Add custom repositories here. Example:
             * options.AddRepository<Question, EfCoreQuestionRepository>();
             */

            options.AddDefaultRepositories(includeAllEntities: true);

        });
    }
}
