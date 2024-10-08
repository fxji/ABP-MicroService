using DataExport.Localization;
using Volo.Abp.Application.Services;

namespace DataExport;

public abstract class DataExportAppService : ApplicationService
{
    protected DataExportAppService()
    {
        LocalizationResource = typeof(DataExportResource);
        ObjectMapperContext = typeof(DataExportApplicationModule);
    }
}
