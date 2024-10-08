using Localization.Resources.AbpUi;
using DataExport.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace DataExport;

[DependsOn(
    typeof(DataExportApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class DataExportHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(DataExportHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<DataExportResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
