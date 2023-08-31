using AAA.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace AAA;

public abstract class AAAController : AbpControllerBase
{
    protected AAAController()
    {
        LocalizationResource = typeof(AAAResource);
    }
}
