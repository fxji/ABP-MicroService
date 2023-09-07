using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace PeBusiness;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(PeBusinessHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class PeBusinessConsoleApiClientModule : AbpModule
{

}
