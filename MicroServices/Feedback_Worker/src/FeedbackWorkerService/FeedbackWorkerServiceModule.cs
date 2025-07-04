using FeedBack;
using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.IdentityModel;
using Volo.Abp.Modularity;


[DependsOn(
    typeof(AbpAutofacModule),
    typeof(FeedBackHttpApiClientModule)
    , typeof(AbpHttpClientIdentityModelModule)
    )]
public class FeedbackWorkerServiceModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        // 读配置文件吧
        // Configure<AbpIdentityClientOptions>(options =>
        //     {
        //         options.IdentityClients.Add("Default", new IdentityClientConfiguration
        //         {
        //             Authority = "http://10.221.128.160:53362",
        //             Scope = "BaseService FeedBackService",
        //             ClientId = "feedback-app",
        //             ClientSecret = "1q2w3e*",
        //             GrantType = "client_credentials",
        //             RequireHttps = false  // ✅ 关键设置
        //         });
        //     });
    }
}