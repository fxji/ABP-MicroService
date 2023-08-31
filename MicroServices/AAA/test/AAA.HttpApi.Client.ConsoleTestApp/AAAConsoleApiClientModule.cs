using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace AAA;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(AAAHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class AAAConsoleApiClientModule : AbpModule
{

}
