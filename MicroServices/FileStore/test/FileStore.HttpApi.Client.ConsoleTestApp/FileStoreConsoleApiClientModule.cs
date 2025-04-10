using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace FileStore;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(FileStoreHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class FileStoreConsoleApiClientModule : AbpModule
{

}
