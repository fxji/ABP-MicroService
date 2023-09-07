using PeBusiness.Localization;
using Volo.Abp.Application.Services;

namespace PeBusiness;

public abstract class PeBusinessAppService : ApplicationService
{
    protected PeBusinessAppService()
    {
        LocalizationResource = typeof(PeBusinessResource);
        ObjectMapperContext = typeof(PeBusinessApplicationModule);
    }
}
