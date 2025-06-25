using avalon.Localization;
using Volo.Abp.Application.Services;

namespace avalon;

public abstract class avalonAppService : ApplicationService
{
    protected avalonAppService()
    {
        LocalizationResource = typeof(avalonResource);
        ObjectMapperContext = typeof(avalonApplicationModule);
    }
}
