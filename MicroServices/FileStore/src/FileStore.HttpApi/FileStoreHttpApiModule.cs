using Localization.Resources.AbpUi;
using FileStore.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace FileStore;

[DependsOn(
    typeof(FileStoreApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class FileStoreHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(FileStoreHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<FileStoreResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
