using DataExport.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace DataExport;

public abstract class DataExportController : AbpControllerBase
{
    protected DataExportController()
    {
        LocalizationResource = typeof(DataExportResource);
    }
}
