using FeedBack.Localization;
using Volo.Abp.Application.Services;

namespace FeedBack;

public abstract class FeedBackAppService : ApplicationService
{
    protected FeedBackAppService()
    {
        LocalizationResource = typeof(FeedBackResource);
        ObjectMapperContext = typeof(FeedBackApplicationModule);
    }
}
