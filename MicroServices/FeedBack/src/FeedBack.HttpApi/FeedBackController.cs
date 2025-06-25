using FeedBack.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace FeedBack;

public abstract class FeedBackController : AbpControllerBase
{
    protected FeedBackController()
    {
        LocalizationResource = typeof(FeedBackResource);
    }
}
