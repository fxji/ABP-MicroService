﻿using Localization.Resources.AbpUi;
using PeBusiness.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace PeBusiness;

[DependsOn(
    typeof(PeBusinessApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class PeBusinessHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(PeBusinessHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<PeBusinessResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
