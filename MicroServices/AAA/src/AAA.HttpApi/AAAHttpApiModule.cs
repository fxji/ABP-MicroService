using Localization.Resources.AbpUi;
using AAA.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace AAA;

[DependsOn(
    typeof(AAAApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class AAAHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(AAAHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<AAAResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
