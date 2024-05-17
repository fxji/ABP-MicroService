using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Emailing;
using Volo.Abp.MailKit;

namespace Email;

[DependsOn(
    typeof(EmailDomainModule),
    typeof(EmailApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule),
    typeof(AbpBackgroundJobsModule),
    typeof(AbpEmailingModule),
    typeof(AbpMailKitModule)
    )]
public class EmailApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<EmailApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<EmailApplicationModule>(validate: true);
        });
        Configure<AbpMailKitOptions>(option =>
        {
            option.SecureSocketOption = MailKit.Security.SecureSocketOptions.StartTlsWhenAvailable;
        });
    }
}
