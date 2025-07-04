using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using Volo.Abp;
using Volo.Abp.BackgroundWorkers;
using Email;

namespace FeedBack;

[DependsOn(
    typeof(EmailApplicationModule),
    typeof(EmailApplicationContractsModule),
    typeof(FeedBackDomainModule),
    typeof(FeedBackApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule)
    
    )]
public class FeedBackApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<FeedBackApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<FeedBackApplicationModule>(validate: false);
        });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        context.AddBackgroundWorkerAsync<ShapeInfoCheckWorker>();
    }



}
