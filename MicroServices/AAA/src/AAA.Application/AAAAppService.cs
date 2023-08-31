using AAA.Localization;
using Volo.Abp.Application.Services;

namespace AAA;

public abstract class AAAAppService : ApplicationService
{
    protected AAAAppService()
    {
        LocalizationResource = typeof(AAAResource);
        ObjectMapperContext = typeof(AAAApplicationModule);
    }
}
