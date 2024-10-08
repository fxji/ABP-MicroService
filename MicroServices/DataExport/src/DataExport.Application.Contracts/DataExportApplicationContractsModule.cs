using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace DataExport;

[DependsOn(
    typeof(DataExportDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class DataExportApplicationContractsModule : AbpModule
{

}
