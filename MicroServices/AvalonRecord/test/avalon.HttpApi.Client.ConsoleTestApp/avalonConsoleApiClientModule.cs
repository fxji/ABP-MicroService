using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace avalon;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(avalonHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class avalonConsoleApiClientModule : AbpModule
{

}
