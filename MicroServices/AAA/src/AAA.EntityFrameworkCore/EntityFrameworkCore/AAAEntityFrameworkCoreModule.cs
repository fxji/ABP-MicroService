using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace AAA.EntityFrameworkCore;

[DependsOn(
    typeof(AAADomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class AAAEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<AAADbContext>(options =>
        {
            /* Add custom repositories here. Example:
             * options.AddRepository<Question, EfCoreQuestionRepository>();
             */


            context.Services.AddAbpDbContext<AAADbContext>(options =>
            {
                options.AddDefaultRepositories(includeAllEntities: true);
            });
        });
    }
}
