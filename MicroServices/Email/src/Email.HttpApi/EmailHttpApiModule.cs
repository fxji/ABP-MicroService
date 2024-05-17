using Localization.Resources.AbpUi;
using Email.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Email;

[DependsOn(
    typeof(EmailApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class EmailHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(EmailHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<EmailResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
