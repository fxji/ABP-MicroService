using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace DataExport;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(DataExportDomainSharedModule)
)]
public class DataExportDomainModule : AbpModule
{

}
