using Localization.Resources.AbpUi;
using avalon.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace avalon;

[DependsOn(
    typeof(avalonApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class avalonHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(avalonHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<avalonResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
