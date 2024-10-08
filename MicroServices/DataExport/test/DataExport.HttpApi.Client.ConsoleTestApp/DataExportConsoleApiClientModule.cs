using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace DataExport;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(DataExportHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class DataExportConsoleApiClientModule : AbpModule
{

}
