using Localization.Resources.AbpUi;
using FeedBack.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace FeedBack;

[DependsOn(
    typeof(FeedBackApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class FeedBackHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(FeedBackHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<FeedBackResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
