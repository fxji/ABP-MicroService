using FeedBack;
using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;


[DependsOn(
    typeof(AbpAutofacModule),
    typeof(FeedBackHttpApiClientModule)
    ,typeof(AbpHttpClientIdentityModelModule)
    )]
public class FeedbackWorkerServiceModule : AbpModule
{
    
}