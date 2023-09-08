using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace PeBusiness.EntityFrameworkCore;

[DependsOn(
    typeof(PeBusinessDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class PeBusinessEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<PeBusinessDbContext>(options =>
        {
            /* Add custom repositories here. Example:
             * options.AddRepository<Question, EfCoreQuestionRepository>();
             */

            context.Services.AddAbpDbContext<PeBusinessDbContext>(options =>
            {
                options.AddDefaultRepositories(includeAllEntities: true);
            });
        });
    }
}
