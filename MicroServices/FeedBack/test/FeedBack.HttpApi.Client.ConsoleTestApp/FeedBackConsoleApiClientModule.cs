using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace FeedBack;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(FeedBackHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class FeedBackConsoleApiClientModule : AbpModule
{

}
