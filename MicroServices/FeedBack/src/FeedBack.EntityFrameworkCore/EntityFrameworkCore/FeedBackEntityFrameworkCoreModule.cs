using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace FeedBack.EntityFrameworkCore;

[DependsOn(
    typeof(FeedBackDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class FeedBackEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<FeedBackDbContext>(options =>
        {
            /* Add custom repositories here. Example:
             * options.AddRepository<Question, EfCoreQuestionRepository>();
             */
            options.AddDefaultRepositories(includeAllEntities: true);
            // context.Services.AddAbpDbContext<FeedBackDbContext>(options =>
            // {
            //     options.AddDefaultRepositories(includeAllEntities: true);
            // });
        });
    }
}
