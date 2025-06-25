using avalon.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace avalon;

public abstract class avalonController : AbpControllerBase
{
    protected avalonController()
    {
        LocalizationResource = typeof(avalonResource);
    }
}
