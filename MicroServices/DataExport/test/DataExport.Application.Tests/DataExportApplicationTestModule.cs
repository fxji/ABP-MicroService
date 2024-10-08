using Volo.Abp.Modularity;

namespace DataExport;

[DependsOn(
    typeof(DataExportApplicationModule),
    typeof(DataExportDomainTestModule)
    )]
public class DataExportApplicationTestModule : AbpModule
{

}
